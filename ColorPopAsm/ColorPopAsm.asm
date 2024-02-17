; Registers
; rcx - Start index
; rdx - End index
; r8 - Threshold
; r9 - Target colors array pointer
; [rsp + 28h] - Target colors array length
; [rsp + 30h] - Bitmap data array pointer
;
; rcx - Enumerator of bitmap data array
; rdx - End of bitmap data array
; r8 - Enumerator of target colors array
; r10 - End of target colors array

.data
indicies dd 0, 8, 2, 10, 4, 12, 6, 14, 1, 9, 3, 11, 5, 13, 7, 15
weights db 20, 125, 58, 0, 20, 125, 58, 0, 20, 125, 58, 0, 20, 125, 58, 0
shuffle_mask db 0, 0, 0, 0, 4, 4, 4, 4, 8, 8, 8, 8, 12, 12, 12, 12
blend_mask db 0, 0, 0, 128, 0, 0, 0, 128, 0, 0, 0, 128, 0, 0, 0, 128, 0, 0, 0, 128, 0, 0, 0, 128, 0, 0, 0, 128, 0, 0, 0, 128

.code
ProcessChunk PROC
	; Initialize mask for blend
	vmovdqa ymm12, YMMWORD PTR [blend_mask]

	; Broadcast threshold value and square
	movd xmm0, r8d
	vpbroadcastd ymm13, xmm0
	vpmulld ymm13, ymm13, ymm13

	; Calculate start address of bitmap data array
	mov rax, [rsp + 30h]
	add rax, rcx
	mov rcx, rax

	; Calculate end address of bitmap data array
	mov rax, [rsp + 30h]
	add rax, rdx
	mov rdx, rax

	; Calculate end address of target colors array
	mov rax, [rsp + 28h]
	add rax, r9
	mov r10, rax

	; Load indicies into zmm11
	vmovdqa32 zmm11, DWORD PTR [indicies]

	; Initialize xmm14 with the weights for luminosity calculation
	movdqa xmm14, XMMWORD PTR [weights]

	; Initialize xmm15 with the shuffle mask
	movdqa xmm15, XMMWORD PTR [shuffle_mask]

_loop:
	; Check if we can process pixels
	add rcx, 32
	cmp rdx, rcx
	js _end
	sub rcx, 32

	; Load colors array pointer into r12
	mov r8, r9								

	; Load data from bitmap data array into ymm0
	vmovdqu ymm0, YMMWORD PTR [rcx]

	; Reset mask
	kxnord k1, k1, k1

_color_loop:
	; Check if we reached the end of the target colors array
	add r8, 4
	cmp r10, r8
	js _color_loop_end
	sub r8, 4

	vpbroadcastd ymm1, DWORD PTR [r8]		; Spread color bytes to the whole register

	; Calculate the difference between the colors
	vpsubusb ymm2, ymm0, ymm1
	vpsubusb ymm3, ymm1, ymm0
	vpmaxub ymm2, ymm2, ymm3

	vpmovzxbw zmm2, ymm2					; Move the result to bigger register
	vpmullw zmm2, zmm2, zmm2				; Square the result

	; Sum the differences for each pixel
	vpermd zmm2, zmm11, zmm2				; Permutate the pixel bytes into lower and upper halves
	vextracti32x8 ymm3, zmm2, 1
	vpmovzxwd zmm3, ymm3
	vextracti32x8 ymm4, zmm2, 0
	vpmovzxwd zmm4, ymm4
	vpaddd zmm2, zmm3, zmm4

	vpermd zmm2, zmm11, zmm2
	vextracti32x8 ymm3, zmm2, 1
	vextracti32x8 ymm4, zmm2, 0
	vpaddd ymm2, ymm3, ymm4

	; Compare the sum of squares with the threshold
	vpcmpgtd k2, ymm2, ymm13
	kandd k1, k2, k2

	add r8, 4
	jmp _color_loop

_color_loop_end:
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

	vinserti128 ymm1, ymm1, xmm2, 1			; Insert result
	vpblendvb ymm1, ymm1, ymm0, ymm12

	; Write luminosity to memory
	vmovdqu32 DWORD PTR [rcx] {k1}, ymm1

	add rcx, 32								; Increment bitmap data array pointer
	jmp _loop								; Jump to the beginning of loop
_end:
	mov rax, 0
	RET
ProcessChunk ENDP
end