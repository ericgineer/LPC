`timescale 1ns/1ns 

module reflect_coeff(input wire signed [31:0] k_tmp,
					 input wire 			v,
					 input wire 			clk,
					 input wire 			rst,
					 output reg signed [15:0] k,
					 output reg signed [15:0] b,
					 output reg 			vout);
			
	reg signed [31:0] k_tmp1, k_tmp2;
	reg signed [15:0] b_tmp1, b_tmp2;
	reg v1, v2;
			
	always @(posedge clk)
	begin
		if (rst)
		begin
			k <= 16'b0;
			b <= 16'b0;
			v1 <= 1'b0;
			v2 <= 1'b0;
		end 
		if (v)
		begin
			k_tmp1 <= k_tmp * 16'h7ff8;
			k_tmp2 <= k_tmp1 + 16'h4000;
			k <= k_tmp2 >>> 15;
			
			b_tmp1 <= k_tmp + 16'h2;
			b_tmp2 <= b_tmp1 >>> 2;
			b <= b_tmp2;
			
			v1 <= v;
			v2 <= v1;
			vout  <= v2;
		end else
		begin
			vout <= 1'b0;
		end
	end
endmodule