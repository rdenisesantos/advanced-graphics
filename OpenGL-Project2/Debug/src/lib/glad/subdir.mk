################################################################################
# Automatically-generated file. Do not edit!
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS += \
../src/lib/glad/glad.c 

OBJS += \
./src/lib/glad/glad.o 

C_DEPS += \
./src/lib/glad/glad.d 


# Each subdirectory must supply rules for building sources it contributes
src/lib/glad/%.o: ../src/lib/glad/%.c
	@echo 'Building file: $<'
	@echo 'Invoking: GCC C Compiler'
	gcc -O0 -g3 -Wall -c -fmessage-length=0 -MMD -MP -MF"$(@:%.o=%.d)" -MT"$(@)" -o "$@" "$<"
	@echo 'Finished building: $<'
	@echo ' '


