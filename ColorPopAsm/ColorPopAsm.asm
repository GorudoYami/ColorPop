; Registers
; rcx - Start index
; rdx - End index
; r8 - Threshold
; r9 - Target colors array pointer
; [rsp + 28h] - Target colors array length
; [rsp + 30h] - Bitmap data array pointer
; r10 - Enumerator of bitmap data array
; r11 - End of bitmap data array
; r12 - Enumerator of target colors array
; r13 - End of target colors array

.data
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

	; Broadcast threshold value and square
	movd xmm0, r8d
	vpbroadcastd zmm13, xmm0
	vpmulld zmm13, zmm13, zmm13

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
	vpsubb ymm2, ymm0, ymm1					; Subtract color bytes from bitmap data
	vpabsb ymm2, ymm2						; Take absolute value of the result

	vpmovzxbw zmm2, ymm2					; Move the result to bigger register
	vpmullw zmm2, zmm2, zmm2				; Square the result

	; Add the squared bytes together
	VPSHUFD zmm3, zmm2, 10110001b
	VPADDD zmm4, zmm2, zmm3

	VPSHUFD zmm3, zmm2, 11001100b
	VPADDD zmm5, zmm2, zmm3

	; Compare the sum of squares with the threshold
	vpcmpud k1, zmm5, zmm13, 5

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

	vmovdqu32 YMMWORD PTR [r10] {k1}, ymm0	; Store new pixels to memory

	add r12, 4								; Increment bitmap data array pointer
	jmp _big_color_loop						; Jump to the beginning of big color loop

_big_color_loop_end:
	add r10, 32								; Increment bitmap data array pointer
	jmp _big_loop							; Jump to the beginning of big loop
_end:
	RET
ProcessChunk ENDP
end