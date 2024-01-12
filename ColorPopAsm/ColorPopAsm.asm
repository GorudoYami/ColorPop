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
; This is probably bad cause the processed image is weird
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

	; Initialize xmm7 with the weights for the R, G, and B channels
	movdqa xmm7, XMMWORD PTR [weights]

	; Initialize xmm8 with the shuffle mask
	movdqa xmm8, XMMWORD PTR [shuffle_mask]

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
	vpmaxub ymm2, ymm0, ymm1				; Get max color bytes
	vpminub ymm3, ymm0, ymm1				; Get min color bytes
	vpsubb ymm4, ymm2, ymm3					; Subtract color bytes from bitmap data

	movzx eax, r8b							; Load threshold into eax
	movd xmm5, eax							; Load threshold into xmm5
	vpbroadcastb ymm5, xmm5					; Spread threshold to the whole register
	vpcmpub k1, ymm4, ymm5, 1				; Compare threshold and absolute result

	; Calculate luminosity for each pixel

	; TODO Fix this
	; Lower half
	vextracti128 xmm6, ymm0, 0				; Extract lower 128 bits (4 pixels) from ymm0
	vpmaddubsw xmm6, xmm6, xmm7				; Multiply each pixel by luminosity coefficients
	vpsrlw xmm6, xmm6, 8					; Shift right to divide by 256
	vpshufb xmm6, xmm6, xmm8				; Shuffle bytes to replicate luminosity
	vinserti128 ymm6, ymm6, xmm6, 1			; Insert lower 128 bits into upper 128 bits

	; Upper half
	vextracti128 xmm6, ymm0, 1				; Extract upper 128 bits (4 pixels) from ymm0
	vpmaddubsw xmm6, xmm6, xmm7				; Multiply each pixel by luminosity coefficients
	vpsrlw xmm6, xmm6, 8					; Shift right to divide by 256
	vpshufb xmm6, xmm6, xmm8				; Shuffle bytes to replicate luminosity
	vinserti128 ymm6, ymm6, xmm6, 0			; Insert upper 128 bits into lower 128 bits

	; Write luminosity to memory
	vmovdqu32 YMMWORD PTR [r10] {k1}, ymm6	; Store luminosity to memory

	add r12, 4								; Increment bitmap data array pointer
	jmp _big_color_loop						; Jump to the beginning of big color loop

_big_color_loop_end:
	add r10, 32								; Increment bitmap data array pointer
	jmp _big_loop							; Jump to the beginning of big loop
_end:
	RET
ProcessChunk ENDP
end