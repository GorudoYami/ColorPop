; Registers
; rcx - Start index
; rdx - End index
; r8 - Threshold
; r9 - Target colors array pointer
; [rsp + 28h] - Target colors array length
; [rsp + 30h] - Bitmap data array pointer
;
; r10 - Enumerator of bitmap data array
; r11 - End of bitmap data array
; r12 - Enumerator of target colors array
; r13 - End of target colors array

.data
indicies_d dd 0, 9, 2, 11, 4, 13, 6, 15, 8, 1, 10, 3, 12, 5, 14, 7
indicies_q dq 0, 4, 2, 6, 1, 5, 3, 7
weights db 38, 75, 15, 0, 38, 75, 15, 0, 38, 75, 15, 0, 38, 75, 15, 0
shuffle_mask db 0, 0, 0, 0, 4, 4, 4, 4, 8, 8, 8, 8, 12, 12, 12, 12

.code
ProcessChunk PROC
	; Calculate start address of bitmap data array
	mov rax, [rsp + 30h]
	add rax, rcx
	mov r10, rax

	; Calculate end address of bitmap data array
	mov rax, [rsp + 30h]
	add rax, rdx
	mov r11, rax

	; Calculate end address of target colors array
	mov rax, [rsp + 28h]
	add rax, r9
	mov r13, rax

	; Load indicies_d into zmm11
	vmovdqa32 zmm11, DWORD PTR [indicies_d]

	; Load indicies_q into zmm12
	vmovdqa64 zmm12, QWORD PTR [indicies_q]

	; Broadcast threshold value and square
	movd xmm0, r8d
	vpbroadcastd ymm13, xmm0
	vpmulld ymm13, ymm13, ymm13

	; Initialize xmm7 with the weights for the R, G, and B channels
	movdqa xmm14, XMMWORD PTR [weights]

	; Initialize xmm8 with the shuffle mask
	movdqa xmm15, XMMWORD PTR [shuffle_mask]

_big_loop:
	mov r12, r9								; Load colors array pointer into r12

	cmp r11, r10							; Check if we reached the end of the bitmap data array
	jz _end									; If yes, then jump to the end
	js _end									; If not but there's still data left, jump to smaller loop

	; Load data from bitmap data array into ymm0
	vmovdqu ymm0, YMMWORD PTR [r10]

_big_color_loop:
	cmp r13, r12							; Check if we reached the end of the target colors array
	jz _big_color_loop_end					; If yes, then jump to the end of this loop

	vpbroadcastd ymm1, DWORD PTR [r12]		; Spread color bytes to the whole register

	; Calculate the difference between the colors
	vpsubusb ymm2, ymm0, ymm1
	vpsubusb ymm3, ymm1, ymm0
	vpmaxub ymm2, ymm2, ymm3

	vpmovzxbw zmm2, ymm2					; Move the result to bigger register
	vpmullw zmm2, zmm2, zmm2				; Square the result

	; Sum the differences for each pixel
	vpermd zmm2, zmm11, zmm2				; Permutate the pixel bytes into lower and upper halves
	vextracti64x4 ymm3, zmm2, 1
	vpmovzxwd zmm3, ymm3
	vextracti64x4 ymm4, zmm2, 0
	vpmovzxwd zmm4, ymm4
	vpaddd zmm2, zmm3, zmm4

	vpermq zmm2, zmm12, zmm2
	vextracti64x4 ymm3, zmm2, 1
	vextracti64x4 ymm4, zmm2, 0
	vpaddd ymm2, ymm3, ymm4

	; Compare the sum of squares with the threshold
	vpcmpgtd k1, ymm2, ymm13

	; Calculate luminosity for each pixel

	; Lower half
	vextracti128 xmm1, ymm0, 0				; Extract lower 128 bits (4 pixels)
	vpmaddubsw xmm1, xmm1, xmm14			; Multiply each pixel by luminosity coefficients
	vpsrlw xmm1, xmm1, 8					; Shift right to divide by 256
	vpshufb xmm1, xmm1, xmm15				; Shuffle bytes to replicate luminosity

	; Upper half
	vextracti128 xmm2, ymm0, 1				; Extract upper 128 bits (4 pixels)
	vpmaddubsw xmm2, xmm2, xmm14			; Multiply each pixel by luminosity coefficients
	vpsrlw xmm2, xmm2, 8					; Shift right to divide by 256
	vpshufb xmm2, xmm2, xmm15				; Shuffle bytes to replicate luminosity

	vinserti128 ymm0, ymm1, xmm2, 1			; Insert result

	; Write luminosity to memory
	vmovdqu32 DWORD PTR [r10] {k1}, ymm0

	add r12, 4								
	jmp _big_color_loop						; Jump to the beginning of big color loop

_big_color_loop_end:
	add r10, 32								; Increment bitmap data array pointer
	jmp _big_loop							; Jump to the beginning of big loop
_end:
	RET
ProcessChunk ENDP
end