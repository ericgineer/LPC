function a = Levinson_durbin(R, p)
	p = p + 1;
	a = int32(zeros(1,p));
	k = int32(zeros(1,p));
	b = k;
	shift = int32(15); % Number of bits to shift right
	round = int32(2^(shift-1));  % amount to add to the value being shifted for rounding
	scale = int32(32760); % 32760
	Rn = int32(0);
	Rd = int32(0);

	a(1) = 8192; % initialize a(1) to 1/4

	% For each order, calculate a new prediction and reflection coefficient
	for m = 2:p
		% Initialize the numerator and denominator accumulators to zero
		Rn = 0;
		Rd = 0;

		% Calculate the numerator and denominator values for the integer division
		for i=1:m
			Rn = Rn + (R(m - i + 1) * a(i));
			Rd = Rd + (R(i) * a(i));
			R(m-i+1)
			R(i)
			a(i)
			fprintf('\n')
		end
		Rn
		Rd
		i

		% Calculate the reflection coefficient k[m]. Round the Q28 number prior
		% to converting it to a Q15 number. Also, scale it by the scaling factor
		% to help keep the input data in proper range.
		k(m) = -Rn / bitshift((Rd + round),-shift);
		k(m) = bitshift(((k(m) * scale) + round),-shift)
		
		% Calculate the new prediction coefficient by converting k[m] from
		% a Q15 to a Q13 number
		b(m) = bitshift((k(m) + 2),-2);
			
		% Calculate the new prediction coefficients for the next iteration
		for i = 1:m-1
			b(i) = bitshift((bitshift(a(i),shift) + (k(m) * a(m - i + 1)) + round),-shift);
		end
		b
		
		% Copy the prediction coefficients from the temporary b[] array to a[]
		for i = 1:m
			a(i) = b(i);
		end
		pause
	end
end
