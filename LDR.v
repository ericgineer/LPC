`timescale 1ns/1ns

module LDR(input wire signed [15:0] R0,
		   input wire signed [15:0] R1,
		   input wire signed [15:0] R2,
		   input wire signed [15:0] R3,
		   input wire signed [15:0] R4,
		   input wire signed [15:0] R5,
		   input wire signed [15:0] R6,
		   input wire signed [15:0] R7,
		   input wire signed [15:0] R8,
		   input wire signed [15:0] R9,
		   input wire signed [15:0] R10,
		   input wire 			  start,
		   input wire 			   clk,
		   input wire 			   rst,
		   output reg signed [15:0] A0,
		   output reg signed [15:0] A1,
		   output reg signed [15:0] A2,
		   output reg signed [15:0] A3,
		   output reg signed [15:0] A4,
		   output reg signed [15:0] A5,
		   output reg signed [15:0] A6,
		   output reg signed [15:0] A7,
		   output reg signed [15:0] A8,
		   output reg signed [15:0] A9,
		   output reg signed [15:0] A10,
		   output reg 				done);
		   
		reg signed [15:0] R0_tmp, R1_tmp, R2_tmp, R3_tmp, R4_tmp, R5_tmp, R6_tmp, R7_tmp, R8_tmp, R9_tmp, R10_tmp;
		
		reg signed [15:0] R0_num, R1_num, R2_num, R3_num, R4_num, R5_num, R6_num, R7_num, R8_num, R9_num, R10_num;
		reg signed [15:0] R0_den, R1_den, R2_den, R3_den, R4_den, R5_den, R6_den, R7_den, R8_den, R9_den, R10_den;
				
		reg signed [15:0] num_tmp0, num_tmp1, num_tmp2, num_tmp3, num_tmp4, num_tmp5, num_tmp6, num_tmp7, num_tmp8;
		reg signed [15:0] a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10;
		wire signed [15:0] a_next0, a_next1, a_next2, a_next3, a_next4, a_next5, a_next6, a_next7, a_next8, a_next9, a_next10;
		
		reg signed [31:0] aR_0, aR_1, aR_2, aR_3, aR_4, aR_5, aR_6, aR_7, aR_8, aR_9, aR_10;
		reg signed [31:0] A0_tmp, A1_tmp, A2_tmp, A3_tmp, A4_tmp, A5_tmp, A6_tmp, A7_tmp, A8_tmp, A9_tmp, A10_tmp;
		   
		reg signed [31:0] A, B;
		wire signed [31:0] R, Q;
		wire signed [31:0] Rn, Rd;
		reg signed [31:0] Rd_tmp, k_tmp;
		reg R_update;
		reg div_rst;
		reg div_start;
		wire div_done;
		wire err;
		
		reg rst_numden, v_numden;
		wire vout_numden;
		
		reg rst_rc, v_rc;
		wire vout_rc;
		wire signed [15:0] k, b;
		
		reg rst_cu, v_cu;
		wire vout_cu;
			   
		reg [4:0] state;
		parameter S0 = 0, S1 = 1, S2 = 2, S3 = 3, S4 = 4, S5 = 5,
				  S6 = 6, S7 = 7, S8 = 8, S9 = 9, S10 = 10, S11 = 11,
				  S12 = 12, S13 = 13, S14 = 14, S15 = 15, S16 = 16,
				  S17 = 17, S18 = 18, S19 = 19, S20 = 20, S21 = 21,
				  S22 = 22, S23 = 23, S24 = 24, S25 = 25, S26 = 26;
				  
		reg run;
				  			  
		divide div(.A(A),
				   .B(B),
				   .start(div_start),
				   .clk(clk),
				   .rst(rst || div_rst),
				   .Q(Q),
				   .R(R),
				   .err(err),
				   .done(div_done));
				   
		NumDen numden(.R_num0(R0_num),
			  .R_num1(R1_num),
			  .R_num2(R2_num),
			  .R_num3(R3_num),
			  .R_num4(R4_num),
			  .R_num5(R5_num),
			  .R_num6(R6_num),
			  .R_num7(R7_num),
			  .R_num8(R8_num),
			  .R_num9(R9_num),
			  .R_num10(R10_num),
			  .R_den0(R0_den),
			  .R_den1(R1_den),
			  .R_den2(R2_den),
			  .R_den3(R3_den),
			  .R_den4(R4_den),
			  .R_den5(R5_den),
			  .R_den6(R6_den),
			  .R_den7(R7_den),
			  .R_den8(R8_den),
			  .R_den9(R9_den),
			  .R_den10(R10_den),
			  .a0(a0),
			  .a1(a1),
			  .a2(a2),
			  .a3(a3),
			  .a4(a4),
			  .a5(a5),
			  .a6(a6),
			  .a7(a7),
			  .a8(a8),
			  .a9(a9),
			  .a10(a10),
			  .v(v_numden),
			  .clk(clk),
			  .rst(rst || rst_numden),
			  .Rn(Rn),
			  .Rd(Rd),
			  .vout(vout_numden));
			  
		reflect_coeff rc(.k_tmp(k_tmp),
						 .v(v_rc),
					     .clk(clk),
					     .rst(rst || rst_rc),
					     .k(k),
					     .b(b),
					     .vout(vout_rc));
						 
		coeff_update cu(.aL_0(A0_tmp),
						.aL_1(A1_tmp),
						.aL_2(A2_tmp),
						.aL_3(A3_tmp),
						.aL_4(A4_tmp),
						.aL_5(A5_tmp),
						.aL_6(A6_tmp),
						.aL_7(A7_tmp),
						.aL_8(A8_tmp),
						.aL_9(A9_tmp),
						.aL_10(A10_tmp),
						.aR_0(aR_0),
						.aR_1(aR_1),
						.aR_2(aR_2),
						.aR_3(aR_3),
						.aR_4(aR_4),
						.aR_5(aR_5),
						.aR_6(aR_6),
						.aR_7(aR_7),
						.aR_8(aR_8),
						.aR_9(aR_9),
						.aR_10(aR_10),
						.k(k),
						.v(v_cu),
						.clk(clk),
						.rst(rst || rst_cu),
						.a_next0(a_next0),
						.a_next1(a_next1),
						.a_next2(a_next2),
						.a_next3(a_next3),
						.a_next4(a_next4),
						.a_next5(a_next5),
						.a_next6(a_next6),
						.a_next7(a_next7),
						.a_next8(a_next8),
						.a_next9(a_next9),
						.a_next10(a_next10),
						.vout(vout_cu));
				   
		// R shift register
		always @(posedge R_update)
		begin
			num_tmp1 <= num_tmp0;
			num_tmp2 <= num_tmp1;
			num_tmp3 <= num_tmp2;
			num_tmp4 <= num_tmp3;
			num_tmp5 <= num_tmp4;
			num_tmp6 <= num_tmp5;
			num_tmp7 <= num_tmp6;
			num_tmp8 <= num_tmp7;
			R0_num <= num_tmp8;
			R1_num <= R0_num;
		    R2_num <= R1_num;
			R3_num <= R2_num;
			R4_num <= R3_num;
			R5_num <= R4_num;
			R6_num <= R5_num;
			R7_num <= R6_num;
			R8_num <= R7_num;
			R9_num <= R8_num;
			R10_num <= R9_num;
		end
		
		
			   
		always @(posedge clk)
		begin
			if (run)
			begin
				R0_tmp <= R0_tmp;
				R1_tmp <= R1_tmp;
				R2_tmp <= R2_tmp;
				R3_tmp <= R3_tmp;
				R4_tmp <= R4_tmp;
				R5_tmp <= R5_tmp;
				R6_tmp <= R6_tmp;
				R7_tmp <= R7_tmp;
				R8_tmp <= R8_tmp;
				R9_tmp <= R9_tmp;
				R10_tmp <= R10_tmp;				
			end else
			begin
				R0_tmp <= R0;
				R1_tmp <= R1;
				R2_tmp <= R2;
				R3_tmp <= R3;
				R4_tmp <= R4;
				R5_tmp <= R5;
				R6_tmp <= R6;
				R7_tmp <= R7;
				R8_tmp <= R8;
				R9_tmp <= R9;
				R10_tmp <= R10;
			end
			
		
		
			if (rst)
			begin
				state <= S0;
			end else
			begin
				case (state)
					S0: if (start)
							state <= S1;
						else
							state <= S0;
					S1: if (S1)
							state <= S2;
						else
							state <= S1;
					S2: if (S2)
							state <= S3;
						else
							state <= S2;
					S3: if (vout_numden)
							state <= S4;
						else
							state <= S3;
					S4: if (S4)
							state <= S5;
						else
							state <= S4;
					S5: if (S5)
							state <= S6;
						else
							state <= S5;
					S6: if (S6)
							state <= S7;
						else
							state <= S6;
					S7: if (S7)
							state <= S8;
						else
							state <= S7;
					S8: if (div_done)
							state <= S9;
						else
							state <= S8;
					S9: if (S9)
							state <= S10;
						else
							state <= S9;
					S10: if (S10)
							state <= S11;
						 else
							state <= S10;
					S11: if (vout_rc)
							state <= S12;
						 else
							state <= S11;
					S12: if (S12)
							state <= S13;
						 else
							state <= S12;
					S13: if(vout_cu)
							state <= S14;
						 else
							state <= S13;
					S14: state <= S14;
				endcase
			end
		end
			
		always @(state)
		begin
			case (state)
				S0: begin
						A1 <= 16'b0;
						A2 <= 16'b0;
						A3 <= 16'b0;
						A4 <= 16'b0;
						A5 <= 16'b0;
						A6 <= 16'b0;
						A7 <= 16'b0;
						A8 <= 16'b0;
						A9 <= 16'b0;
						A10 <= 16'b0;
						run <= 1'b0;
						done <= 1'b0;
						div_rst <= 1'b0;
						div_start <= 1'b0;
						v_numden <= 1'b0;
						v_rc <= 1'b0;
						v_cu <= 1'b0;
						rst_numden <= 1'b0;
						rst_rc <= 1'b0;
						rst_cu <= 1'b0;
						A <= 1'b0;
						B <= 1'b0;
						R0_num <= 16'b0;
						R1_num <= 16'b0;
						R2_num <= 16'b0;
						R3_num <= 16'b0;
						R4_num <= 16'b0;
						R5_num <= 16'b0;
						R6_num <= 16'b0;
						R7_num <= 16'b0;
						R8_num <= 16'b0;
						R9_num <= 16'b0;
						R10_num <= 16'b0;
						R0_den <= 16'b0;
						R1_den <= 16'b0;
						R2_den <= 16'b0;
						R3_den <= 16'b0;
						R4_den <= 16'b0;
						R5_den <= 16'b0;
						R6_den <= 16'b0;
						R7_den <= 16'b0;
						R8_den <= 16'b0;
						R9_den <= 16'b0;
						R10_den <= 16'b0;
						num_tmp0 <= 16'b0;
						num_tmp1 <= 16'b0;
						num_tmp2 <= 16'b0;
						num_tmp3 <= 16'b0;
						num_tmp4 <= 16'b0;
						num_tmp5 <= 16'b0;
						num_tmp6 <= 16'b0;
						num_tmp7 <= 16'b0;
						num_tmp8 <= 16'b0;
						a0 <= 16'b0;
						a1 <= 16'b0;
						a2 <= 16'b0;
						a3 <= 16'b0;
						a4 <= 16'b0;
						a5 <= 16'b0;
						a6 <= 16'b0;
						a7 <= 16'b0;
						a8 <= 16'b0;
						a9 <= 16'b0;
						a10 <= 32'b0;
						aR_0 <= 32'b0;
						aR_1 <= 32'b0;
						aR_2 <= 32'b0;
						aR_3 <= 32'b0;
						aR_4 <= 32'b0;
						aR_5 <= 32'b0;
						aR_6 <= 32'b0;
						aR_7 <= 32'b0;
						aR_8 <= 32'b0;
						aR_9 <= 32'b0;
						aR_10 <= 32'b0;
						A0_tmp <= 32'b0;
						A1_tmp <= 32'b0;
						A2_tmp <= 32'b0;
						A3_tmp <= 32'b0;
						A4_tmp <= 32'b0;
						A5_tmp <= 32'b0;
						A6_tmp <= 32'b0;
						A7_tmp <= 32'b0;
						A8_tmp <= 32'b0;
						A9_tmp <= 32'b0;
						A10_tmp <= 32'b0;
					end
				S1: begin // Iteration 1
						run <= 1'b1;
					end
				S2: begin
						num_tmp0 <= R10_tmp;
						num_tmp1 <= R9_tmp;
						num_tmp2 <= R8_tmp;
						num_tmp3 <= R7_tmp;
						num_tmp4 <= R6_tmp;
						num_tmp5 <= R5_tmp;
						num_tmp6 <= R4_tmp;
						num_tmp7 <= R3_tmp;
						num_tmp8 <= R2_tmp;
						R0_num   <= R1_tmp;
						R1_num   <= R0_tmp;
						R0_den <= R0_tmp;
						R1_den <= R1_tmp;
						a0 <= 16'd8192;
						A0_tmp <= 32'd8192;
						v_numden <= 1'b1;
					end
				S3: begin
						v_numden <= 1'b0;
					end
				S4: Rd_tmp <= Rd + 16'h4000;
				S5: Rd_tmp <= Rd_tmp >>> 15;
				S6: begin
						A <= -Rn;
						B <= Rd_tmp;
					end
				S7: div_start <= 1'b1;
				S8: div_start <= 1'b0;
				S9: k_tmp <= Q;
				S10: v_rc <= 1'b1;
				S11: v_rc <= 1'b0;
				S12: v_cu <= 1'b1;
				S13: v_cu <= 1'b0;
				S14: begin 
						A0_tmp <= a_next0;
						A1_tmp <= b;
					 end
			endcase
		end
endmodule
			
			   
			   