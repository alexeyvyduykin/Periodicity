namespace PRDCT.Core.TEST
{


    //    public class UnitEilP
    //    {
    //        // { for the procedures
    //        //    of the generalised two fixed centres problem }

    //        public void CRONA(ref double[] C)
    //        {
    //            double FM = C[0];
    //            double CJ2 = C[2];
    //            double SIG = C[3];
    //            double A = C[7];
    //            double E = C[8];
    //            double S = C[9];
    //            double AC = Math.Sqrt(Math.Abs(1.0e0 - S * S));
    //            if (C[6] < 0.0e0)
    //                AC = -AC;
    //            double O = 1.0e0;
    //            double O2 = 2.0e0;
    //            double O3 = 3.0e0;
    //            double O4 = 0.25e0;
    //            double O5 = 0.5e0;
    //            double E2 = E * E;
    //            double RE = O - E2;
    //            double E4 = E2 * E2;
    //            double S2 = S * S;
    //            double RS = O - S2;
    //            double S4 = S2 * S2;
    //            double P = CJ2 / (A * RE);
    //            double P2 = P * P;
    //            double PS = P * SIG;
    //            double Z2 = SIG * SIG;
    //            double AX = E * (O + P2 * RE * (O - O2 * S2 + P2 * (O3 - 16.0e0 * S2 + 14.0e0 * S4 - O2 * E2 * RS * RS)));
    //            C[13] = AX;
    //            double AG = -PS * (O - O2 * S2 - P2 * (O3 - 12.0e0 * S2 + 10.0e0 * S4 + E2 * (O - O2 * S4)));
    //            double AT = PS * S * (O - P2 * (5.0e0 - 6.0e0 * S2 - E2 * (O - O2 * S2)));
    //            C[14] = AG;
    //            C[15] = AT;
    //            double AK1 = 0.125e0 * P2 * S2 * (RE + Z2 - 4.0e0 * P2 * RE * RS);
    //            double AK2 = 0.125e0 * P2 * E2 * (S2 - P2 * (O - 10.0e0 * S2 + 11.0e0 * S4 + E2 * S4));
    //            C[25] = AK1;
    //            C[26] = AK2;
    //            double PM = Math.Sqrt(FM * A * RE);
    //            P2 = O5 * P2;
    //            double P4 = O5 * P2 * P2;
    //            double AG1 = PM * (O + P2 * (RS * (O3 + E2) + Z2 * (6.0e0 - 7.0e0 * S2))
    //                 - P4 * RS * (9.0e0 + 11.0e0 * S2 + E2 * (6.0e0 + 34.0e0 * S2) + E4 * (O + O3 * S2)));
    //            double AG2 = PM * (O - P2 * (O3 - 4.0e0 * S2 - E2) - P4 * (9.0e0 - 72.0e0 * S2
    //                 + 64.0e0 * S4 + E2 * (O2 - 40.0e0 * S2 + 48.0e0 * S4) + E4));
    //            double AN = AG1 * (O + O2 * AK2 + 9.0e0 * AK2 * AK2) / (AG2 * (O + O2 * AK1 + 9.0e0 * AK1 * AK1));
    //            C[16] = AG1;
    //            C[17] = AG2;
    //            C[18] = AN;
    //            PS = PS * S * AC;
    //            P2 = P * P;
    //            P4 = P2 / 16.0e0;
    //            C[19] = O2 * PS * (O - P2 * (4.0e0 - 5.0e0 * S2 + E2 * S2));
    //            double R1 = P2 * AC;
    //            double R2 = O2 * P4;
    //            double R3 = 4.0e0 * R2;
    //            double R4 = O4 * R1 * P4;
    //            double AC0 = -R1 * (O + O5 * E2 + P4 * (24.0e0 - 56.0e0 * S2 - 4.0e0 * E2 * (O + 16.0e0 * S2) - E4 * (O2 + O3 * S2)));
    //            C[20] = AC0;
    //            C[21] = -O2 * R1 * E * (O + R2 * (4.0e0 - 28.0e0 * S2 - E2 * (6.0e0 + 7.0e0 * S2)));
    //            C[22] = -O4 * R1 * E2 * (O - R3 * (11.0e0 + E2 * (O + S2)));
    //            C[23] = 16.0e0 * R4 * E * E2 * (O2 - S2);
    //            C[24] = R4 * E4 * (O2 + S2);
    //            double AQ0 = -O5 * R1 * (RE + O3 * Z2 - O2 * P4 * RE * (30.0e0 - 35.0e0 * S2 + E2 * (O2 + O3 * S2)));
    //            C[27] = AQ0;
    //            C[28] = P2 * PS * RE;
    //            C[29] = 6.0e0 * R4 * S2 * RE * RE;
    //            double F2 = O - R3 * RE * (4.0e0 - 7.0e0 * S2) - P2 * R2 * RE
    //                * (20.0e0 - 136.0e0 * S2 + 113.0e0 * S4 - E2 * (8.0e0 + 24.0e0 * S2 - 47.0e0 * S4));
    //            double F1 = P2 * RE * (O2 - O3 * S2 + R3 * (O2 - 22.0e0 * S2 + 19.0e0 * S4
    //                    - E2 * S2 * (10.0e0 - 13.0e0 * S2)));
    //            double F0 = O / (F1 + F2);
    //            F0 = F0 / Math.Sqrt(O - AX * AX);
    //            F0 = F0 * RE * RE;
    //            double B0 = R3 * (-S2 + R3 * (6.0e0 - 20.0e0 * S2 + 15.0e0 * S4 - E2 * S4));
    //            double B1 = -P2 * R3 * S2 * (O2 - S2);
    //            double B2 = O3 * P2 * R2 * S4;
    //            double AP0 = F0 * (B0 + B1 + B2 * (O + O5 * AX * AX));
    //            C[31] = AP0;
    //            C[32] = AX * (B1 + O2 * B2) * F0;
    //            C[33] = O4 * E2 * B2 * F0;
    //            F0 = P2 * F0 * AG2 / AG1;
    //            double AF0 = O5 * F0 * S2 * (O + O3 * AK1);
    //            C[34] = AF0;
    //            C[35] = -F0 * S * (O2 * AG - 1.5e0 * S * AT);
    //            C[37] = -F0 * S2 * AT / 6.0e0;
    //            C[36] = -F0 * S2 * (O4 + AK1);
    //            C[38] = 0.125e0 * F0 * S2 * AK1;
    //            C[39] = -AP0 - AN * AF0;
    //            C[44] = AN - O;
    //            C[41] = C[40] / (O - C[39]);
    //            C[42] = C[44] * C[41];
    //            C[45] = AC0 + AN * AQ0;
    //            C[43] = C[41] * C[45];
    //            C[46] = AK1 * (O + 4.0e0 * AK1);
    //            C[47] = -0.75e0 * AK1 * AK1;
    //            C[48] = -AN * AK2 * (O + 4.0e0 * AK2);
    //            C[49] = 0.75e0 * AN * AK2 * AK2;
    //            C[22] = C[22] + AQ0 * C[48];
    //            C[29] = C[29] + AQ0 * C[46];
    //            C[33] = C[33] + AF0 * C[48];
    //            C[36] = C[36] + AF0 * C[46];
    //        }

    //        public void ElemC(TElemRec ElemR, /* type TElemRec from UnCoTVar*/ ref double[] C)
    //        {
    //            double RD = 0.0, RDZ = 0.0, R1 = 0.0, R2 = 0.0;
    //            double P2 = 0.0, P4 = 0.0;
    //            double FM = 3.98600448000e+5;
    //            double RZ = 6.37813700000e+3;
    //            double CJ2 = 2.09728850857e+2;
    //            double SIG = -3.55889244242e-2;
    //            double AN0 = 2.0 * Math.PI * ElemR.an / 86400.0;
    //            double S = MyMath.DegreesToRadians * ElemR.ai; // constants from unit UnConTyp
    //            double ASZ = Math.Sin(S);
    //            double AC = Math.Cos(S);
    //            double AD = ASZ;
    //            double ADZ = -ASZ;
    //            double A1 = -Math.Exp(2.0e0 / 3.0e0 * SimpleProcedures.Log10(FM * AN0));
    //            double F0 = -FM / A1;
    //            double AB = F0;
    //            double FC = CJ2 / F0;
    //            double RE = 1.0e0 - ElemR.ae * ElemR.ae;
    //            double F1 = 2.0e0 - RE;
    //            double F2 = RE * RE;
    //            for (int j = 0; j < 4; j++)
    //            {
    //                double PE = CJ2 / (AB * RE);
    //                P2 = PE * PE;
    //                P4 = P2 * P2;
    //                double PS = PE * SIG;
    //                double D2 = AD * AD;
    //                RD = 1.0e0 - D2;
    //                double D4 = D2 * D2;
    //                double RC = 1.0e0 - 2.0e0 * PS * AD - P2 * D2 * RE;
    //                double RR = 1.0e0 + 2.0e0 * P2 * D2 * F1 + P4 * D4 * F2;
    //                R1 = RC / RR;
    //                double F3 = 1.0e0 - RD * R1;
    //                AB = F0 * (1.0e0 - P2 * RE * RD * R1);
    //                double DZ2 = ADZ * ADZ;
    //                RDZ = 1.0e0 - DZ2;
    //                double DZ4 = DZ2 * DZ2;
    //                double RCZ = 1.0e0 - 2.0e0 * PS * ADZ - P2 * DZ2 * RE;
    //                double RRZ = 1.0e0 + 2.0e0 * P2 * DZ2 * F1 + P4 * DZ4 * F2;
    //                R2 = RCZ / RRZ;
    //                double F4 = 1.0e0 - RDZ * R2;
    //                double F5 = F3 + D2 * ADZ * (FC * PE * (2.0e0 * AD + ADZ) + 2.0e0 * PS);
    //                double F6 = F4 + DZ2 * AD * (FC * PE * (2.0e0 * ADZ + AD) + 2.0e0 * PS);
    //                double F7 = Math.Sqrt(Math.Abs((F5 / F6) * (F4 / F3)));
    //                double F9 = R1 / R2;
    //                double F8 = 2.0e0 * F7 * (1.0e0 + F7) * F9 * ASZ;
    //                ADZ = (-1.0e0 + F9 - F9 * Math.Pow((1.0e0 + F7) * ASZ, 2) + (1.0e0 - F9 * F7 * F7) * DZ2) / F8;
    //                AD = ASZ * (1.0e0 + F7) + F7 * ADZ;
    //            }
    //            double R3 = FM * AB * RE * RD * (1.0e0 + 2.0e0 * P2 * F1 + P4 * F2) * R1;
    //            double R4 = FM * AB * RE * RDZ * (1.0e0 + 2.0e0 * P2 * F1 + P4 * F2) * R2;
    //            double A3 = Math.Sqrt(Math.Abs(R3));
    //            if (AC < 0.0e0)
    //                A3 = -A3;
    //            double A2 = -FM * AB * RE * (1.0e0 + 2.0e0 * P2 * F1 * RD * R1 + P4 * F2 * RD * R1);
    //            C[7] = AB;
    //            C[8] = ElemR.ae;
    //            C[9] = ASZ;
    //            C[10] = AC;
    //            C[4] = A1;
    //            C[5] = A2;
    //            C[6] = A3;
    //            C[11] = AD;
    //            C[12] = ADZ;
    //            C[0] = FM;
    //            C[1] = RZ;
    //            C[2] = CJ2;
    //            C[3] = SIG;
    //            C[30] = -C[7] * C[8] * A1 / FM;
    //            C[40] = AN0;
    //            CRONA(ref C);
    //        }

    //        public void ElemX(double TCur, TElemRec ElemR, out dvec3 X)
    //        {
    //            double[] C = new double[50];
    //            ElemC(ElemR, ref C);
    //            double ddl = 2.0 * Math.PI * 0.5 * ElemR.dn * Math.Pow(TCur - ElemR.t, 2); // +1/2 dn/dt (t-t_0)^2 radian
    //            double DifT = 86400.0 * (TCur - ElemR.t); //{ in second }
    //            double UDL = MyMath.DegreesToRadians * ElemR.am + C[41] * DifT + ddl; //{ in radian }
    //            double UDG = MyMath.DegreesToRadians * ElemR.ao + C[42] * DifT + Math.PI / 2.0;
    //            double UDH = MyMath.DegreesToRadians * ElemR.au + C[43] * DifT - Math.PI / 2.0;
    //            double O = 1.0e0;
    //            double W = 2.00000e0;
    //            double OL = 0.0e0;
    //            double FM = C[0];
    //            double RZ = C[1];
    //            double CJ2 = C[2];
    //            double SIG = C[3];
    //            double A1 = C[4];
    //            double A2 = C[5];
    //            double A3 = C[6];
    //            double AB = C[7];
    //            double AE = C[8];
    //            double ASZ = C[9];
    //            double AC = C[10];
    //            double AD = C[11];
    //            double ADZ = C[12];
    //            double AX = C[13];
    //            double AG = C[14];
    //            double AT = C[15];
    //            double AG1 = C[16];
    //            double AG2 = C[17];
    //            double AN = C[18];
    //            double AQ = C[19];
    //            double AC0 = C[20];
    //            double AC1 = C[21];
    //            double AC2 = C[22];
    //            double AC3 = C[23];
    //            double AC4 = C[24];
    //            double AK1 = C[25];
    //            double AK2 = C[26];
    //            double AQ0 = C[27];
    //            double AQ1 = C[28];
    //            double AQ2 = C[29];
    //            double AZ = C[30];
    //            double AP0 = C[31];
    //            double AP1 = C[32];
    //            double AP2 = C[33];
    //            double AF0 = C[34];
    //            double AF1 = C[35];
    //            double AF2 = C[36];
    //            double AF3 = C[37];
    //            double AF4 = C[38];
    //            double ALAM = C[39];
    //            double AN0 = C[40];
    //            double OH = C[44];
    //            double AMU = C[45];
    //            double AKF2 = C[46];
    //            double AKF4 = C[47];
    //            double AKP2 = C[48];
    //            double AKP4 = C[49];
    //            double RXX = 1.0e0 - AX * AX;
    //            double RX = Math.Sqrt(RXX);
    //            double Z2 = CJ2 * CJ2;
    //            double Z0 = CJ2 * SIG;
    //            double EA = KeplerEquation(AZ, UDL); //{ eccentric anomaly in radian }
    //            double SE = Math.Sin(EA);
    //            double CE = Math.Cos(EA);
    //            double RB = O / (O - AX * CE);
    //            double SP = RX * RB * SE;
    //            double CP = (CE - AX) * RB;
    //            double SPP = W * SP * CP;
    //            double CPP = CP * CP - SP * SP;
    //            double SPR = W * SPP * CPP;
    //            double EP = EA + Math.Atan((SP * CE - CP * SE) / (CP * CE + SP * SE));
    //            double PUDL = EP - UDL;
    //            double EF = EP + UDG + OH * (EP - UDL);
    //            double SF = Math.Sin(EF);
    //            double CF = Math.Cos(EF);
    //            double SFF = W * SF * CF;
    //            int j = 0;
    //            double DE = 1.0e0;
    //            while (j < 15 && DE > 1.0e-15)
    //            {
    //                j = j + 1;
    //                PUDL = EP - UDL;
    //                SPP = W * SP * CP;
    //                CPP = CP * CP - SP * SP;
    //                SPR = W * SPP * CPP;
    //                SFF = W * SF * CF;
    //                double CFF = CF * CF - SF * SF;
    //                double SFR = W * SFF * CFF;
    //                double EE = UDL + AZ * SE + ALAM * PUDL - AP1 * SP - AP2 * SPP
    //                             - AF1 * SF + AF2 * SFF + AF3 * (SF * CFF + CF * SFF) - AF4 * SFR;
    //                SE = Math.Sin(EE);
    //                CE = Math.Cos(EE);
    //                RB = O / (O - AX * CE);
    //                SP = RX * RB * SE;
    //                CP = (CE - AX) * RB;
    //                EP = EE + Math.Atan((SP * CE - CP * SE) / (CP * CE + SP * SE));
    //                EF = EP + UDG + OH * PUDL + AKP2 * SPP + AKP4 * SPR - AKF2 * SFF + AKF4 * SFR;
    //                SF = Math.Sin(EF);
    //                CF = Math.Cos(EF);
    //                DE = Math.Abs(EA - EE);
    //                EA = EE;
    //            }
    //            double BS = AB * (O - AE * CE);
    //            double RT = O / (O - AT * CF);
    //            double BT = RT * (-ASZ * CF + AG);
    //            double R1 = BS * BS + Z2;
    //            double R2 = O - BT * BT;
    //            double R3 = Math.Sqrt(R1 * R2);
    //            double R4 = -AC * CF + AQ;
    //            double R6 = O / (R4 * R4 + SF * SF);
    //            double R5 = Math.Sqrt(R6);
    //            double BW = UDH + (Math.PI / 2.0) + AMU * PUDL + AQ1 * SF - AQ2 * SFF;
    //            BW = BW + AC1 * SP + AC2 * SPP + AC3 * (SP * CPP + CP * SPP) + AC4 * SPR;
    //            double SW = R4 * R5;
    //            double CW = SF * R5;
    //            double SU = Math.Sin(BW);
    //            double CU = Math.Cos(BW);
    //            X = new dvec3
    //            {
    //                x = R3 * (CW * CU - SW * SU),
    //                y = R3 * (CW * SU + SW * CU),
    //                z = Z0 + BS * BT
    //            };
    //        }

    //        public void EilXV(double TCur, TElemRec ElemR, out dvec3 pos, out dvec3 vel)
    //        {
    //            ElemC(ElemR, C);
    //            double ddl = 2.0 * Math.PI * 0.5 * ElemR.dn * Math.Pow(TCur - ElemR.t, 2); // +1/2 dn/dt (t-t_0)^2 radian
    //            double DifT = 86400.0 * (TCur - ElemR.t); // { in second }
    //            double UDL = MyMath.DegreesToRadians * ElemR.am + C[41] * DifT + ddl; // { in radian }
    //            double UDG = MyMath.DegreesToRadians * ElemR.ao + C[42] * DifT + (Math.PI / 2.0);
    //            double UDH = MyMath.DegreesToRadians * ElemR.au + C[43] * DifT - (Math.PI / 2.0);
    //            double O = 1.0e0;
    //            double W = 2.00000e0;
    //            double OL = 0.0e0;
    //            double FM = C[0];
    //            double RZ = C[1];
    //            double CJ2 = C[2];
    //            double SIG = C[3];
    //            double A1 = C[4];
    //            double A2 = C[5];
    //            double A3 = C[6];
    //            double AB = C[7];
    //            double AE = C[8];
    //            double ASZ = C[9];
    //            double AC = C[10];
    //            double AD = C[11];
    //            double ADZ = C[12];
    //            double AX = C[13];
    //            double AG = C[14];
    //            double AT = C[15];
    //            double AG1 = C[16];
    //            double AG2 = C[17];
    //            double AN = C[18];
    //            double AQ = C[19];
    //            double AC0 = C[20];
    //            double AC1 = C[21];
    //            double AC2 = C[22];
    //            double AC3 = C[23];
    //            double AC4 = C[24];
    //            double AK1 = C[25];
    //            double AK2 = C[26];
    //            double AQ0 = C[27];
    //            double AQ1 = C[28];
    //            double AQ2 = C[29];
    //            double AZ = C[30];
    //            double AP0 = C[31];
    //            double AP1 = C[32];
    //            double AP2 = C[33];
    //            double AF0 = C[34];
    //            double AF1 = C[35];
    //            double AF2 = C[36];
    //            double AF3 = C[37];
    //            double AF4 = C[38];
    //            double ALAM = C[39];
    //            double AN0 = C[40];
    //            double OH = C[44];
    //            double AMU = C[45];
    //            double AKF2 = C[46];
    //            double AKF4 = C[47];
    //            double AKP2 = C[48];
    //            double AKP4 = C[49];
    //            double RXX = 1.0e0 - AX * AX;
    //            double RX = Math.Sqrt(RXX);
    //            double Z2 = CJ2 * CJ2;
    //            double Z0 = CJ2 * SIG;

    //            double EA = KeplerEquation(AZ, UDL); //{ eccentric anomaly in radian }
    //            double SE = Math.Sin(EA);
    //            double CE = Math.Cos(EA);
    //            double RB = O / (O - AX * CE);
    //            double SP = RX * RB * SE;
    //            double CP = (CE - AX) * RB;
    //            double SPP = W * SP * CP;
    //            double CPP = CP * CP - SP * SP;
    //            double SPR = W * SPP * CPP;
    //            double EP = EA + Math.Atan((SP * CE - CP * SE) / (CP * CE + SP * SE));
    //            double PUDL = EP - UDL;
    //            double EF = EP + UDG + OH * (EP - UDL);
    //            double SF = Math.Sin(EF);
    //            double CF = Math.Cos(EF);
    //            double SFF = W * SF * CF;
    //            int j = 0;
    //            double DE = 1.0e0;
    //            while (j < 15 && DE > 1.0e-15)
    //            {
    //                j = j + 1;
    //                PUDL = EP - UDL;
    //                SPP = W * SP * CP;
    //                CPP = CP * CP - SP * SP;
    //                SPR = W * SPP * CPP;
    //                SFF = W * SF * CF;
    //                double CFF = CF * CF - SF * SF;
    //                double SFR = W * SFF * CFF;
    //                double EE = UDL + AZ * SE + ALAM * PUDL - AP1 * SP - AP2 * SPP - AF1 * SF + AF2 * SFF + AF3 * (SF * CFF + CF * SFF) - AF4 * SFR;
    //                SE = Math.Sin(EE);
    //                CE = Math.Cos(EE);
    //                RB = O / (O - AX * CE);
    //                SP = RX * RB * SE;
    //                CP = (CE - AX) * RB;
    //                EP = EE + Math.Atan((SP * CE - CP * SE) / (CP * CE + SP * SE));
    //                EF = EP + UDG + OH * PUDL + AKP2 * SPP + AKP4 * SPR - AKF2 * SFF + AKF4 * SFR;
    //                SF = Math.Sin(EF);
    //                CF = Math.Cos(EF);
    //                DE = Math.Abs(EA - EE);
    //                EA = EE;
    //            }
    //            double BS = AB * (O - AE * CE);
    //            double RT = O / (O - AT * CF);
    //            double BT = RT * (-ASZ * CF + AG);
    //            double R1 = BS * BS + Z2;
    //            double R2 = O - BT * BT;
    //            double R3 = Math.Sqrt(R1 * R2);
    //            double R4 = -AC * CF + AQ;
    //            double R6 = O / (R4 * R4 + SF * SF);
    //            double R5 = Math.Sqrt(R6);
    //            double BW = UDH + (Math.PI / 2.0) + AMU * PUDL + AQ1 * SF - AQ2 * SFF;
    //            BW = BW + AC1 * SP + AC2 * SPP + AC3 * (SP * CPP + CP * SPP) + AC4 * SPR;
    //            double SW = R4 * R5;
    //            double CW = SF * R5;
    //            double SU = Math.Sin(BW);
    //            double CU = Math.Cos(BW);
    //            pos.x = R3 * (CW * CU - SW * SU);
    //            pos.y = R3 * (CW * SU + SW * CU);
    //            pos.z = Z0 + BS * BT;
    //            double ri = o / (bs * bs + z2 * bt * bt);
    //            double r1 = o / r1;
    //            double r2 = o / r2;
    //            double q1 = Math.Sqrt(o - 8 * ak2 * sp * sp);
    //            double q2 = Math.Pow(rx / (o + ax * cp), 2);
    //            double q3 = ab * ae * ag2;
    //            double vs = q3 * ri * q2 * sp * q1;
    //            q1 = Math.Sqrt(o - 8 * ak1 * cf * cf);
    //            q2 = (asz - ag * at) * ag1;
    //            double vt = q2 * ri * rt * rt * sf * q1;
    //            double vw = a3 * r1 * r2;
    //            double r4 = bs * vs * r1 - bt * vt * r2;
    //            vel.x = pos.x * r4 - pos.y * vw;
    //            vel.y = pos.y * r4 + pos.x * vw;
    //            vel.z = bs * vt + vs * bt;
    //        }

    //        public void ALEI(ref double[] C)
    //        {
    //            double O = 1.0e0;
    //            double A1 = C[4];
    //            double A2 = C[5];
    //            double A3 = C[6];
    //            double FM = C[0];
    //            double CJ2 = C[2];
    //            double SIG = C[3];
    //            double AB = -FM / A1;
    //            double FA = AB;
    //            double FE = -A2 / FM;
    //            double RE = FE / FA;
    //            double PA = A3 * A3;
    //            double RD = -PA / A2;
    //            double RDZ = RD;
    //            double FD = RD;
    //            PA = PA / FM;
    //            for (int j = 0; j < 5; j++)
    //            {
    //                double PE = CJ2 / (AB * RE);
    //                double P2 = PE * PE;
    //                double PS = PE * SIG;
    //                double D2 = O - RD;
    //                double AD = Math.Sqrt(Math.Abs(D2));
    //                double P1 = 2.0e0 * P2 * (2.0e0 - RE);
    //                P2 = P2 * RE;
    //                double P4 = P2 * P2;
    //                double RR = O + P1 * D2 + P4 * D2 * D2;
    //                double RC = O - 2.0e0 * PS * AD - P2 * D2;
    //                RR = RC / RR;
    //                double RF = (O + P1 + P4) * RR;
    //                double RT = O + (RF - RR) * RD;
    //                AB = FA * (O - P2 * RD * RR);
    //                RE = FE / (AB * RT);
    //                RD = FD * RT / RF;
    //                double DZ2 = O - RDZ;
    //                double ADZ = -Math.Sqrt(Math.Abs(DZ2));
    //                double RRZ = O + P1 * DZ2 + P4 * DZ2 * DZ2;
    //                double RCZ = O - 2.0e0 * PS * ADZ - P2 * DZ2;
    //                RDZ = PA * RRZ / (AB * RE * (O + P1 + P4) * RCZ);
    //            }
    //            double E2 = O - RE;
    //            C[7] = AB;
    //            C[8] = Math.Sqrt(Math.Abs(E2));
    //            double D2 = O - RD;
    //            C[11] = Math.Sqrt(Math.Abs(D2));
    //            double DZ2 = O - RDZ;
    //            C[12] = -Math.Sqrt(Math.Abs(DZ2));
    //            double P2 = PE * PE * (3.0e0 - 4.0e0 * D2 + E2);
    //            C[9] = C[11] + PS * RD * (O - 3.0e0 * PS * C[11] - P2);
    //            C[10] = Math.Sqrt(Math.Abs(O - C[9] * C[9]));
    //            if (A3 < 0.0e0)
    //                C[10] = -C[10];
    //            double RF = O / FM;
    //            double R = -A1 * A1 * A1;
    //            C[40] = Math.Sqrt(R) * RF;
    //            C[30] = -C[7] * C[8] * A1 * RF;
    //        }

    //        public void XVEilR(double t, double[] X, out TElemRec ElemR) // UnCoTVar
    //        {
    //            double[] C = new double[50];
    //            double FM = 3.98600448000e+5;
    //            double RZ = 6.37813700000e+3;
    //            double CJ2 = 2.09728850857e+2;
    //            double SIG = -3.55889244242e-02;
    //            double Z2 = CJ2 * CJ2;
    //            double Z0 = CJ2 * SIG;
    //            double X1 = X[0];
    //            double X2 = X[1];
    //            double X3 = X[2] - Z0;
    //            double V1 = X[3];
    //            double V2 = X[4];
    //            double V3 = X[5];
    //            double ZX = X3 * X3;
    //            double R2 = X1 * X1 + X2 * X2 + ZX;
    //            double U2 = V1 * V1 + V2 * V2 + V3 * V3;
    //            double R1 = X1 * V1 + X2 * V2 + X3 * V3;
    //            double F2 = 0.5e0 * (R2 - Z2 + Math.Sqrt(Math.Pow(R2 - Z2, 2) + 4.0e0 * Z2 * ZX));
    //            double BS = Math.Sqrt(F2);
    //            double BT = X3 / BS;
    //            double Z4 = Z2 * (BT * BT);
    //            double FI = 2.0e0 * FM / (F2 + Z4);
    //            double VT = BS * V3 - BT * R1;
    //            double VS = BS * R1 + Z2 * BT * V3;
    //            double A1 = U2 - FI * (BS - Z0 * BT);
    //            double A3 = X1 * V2 - V1 * X2;
    //            double A2 = R1 * R1 - R2 * U2 + Z2 * V3 * V3 - FI * (BS * Z4 + Z0 * BT * F2);
    //            C[0] = FM;
    //            C[1] = RZ;
    //            C[2] = CJ2;
    //            C[3] = SIG;
    //            C[4] = A1;
    //            C[5] = A2;
    //            C[6] = A3;
    //            ALEI(ref C);
    //            CRONA(ref C);
    //            double AB = C[7];
    //            double AE = C[8];
    //            double ASZ = C[9];
    //            double AC = C[10];
    //            double AD = C[11];
    //            double ADZ = C[12];
    //            double AX = C[13];
    //            double AG = C[14];
    //            double AT = C[15];
    //            double AG1 = C[16];
    //            double AG2 = C[17];
    //            double AN = C[18];
    //            double AQ = C[19];
    //            double AC0 = C[20];
    //            double AC1 = C[21];
    //            double AC2 = C[22];
    //            double AC3 = C[23];
    //            double AC4 = C[24];
    //            double AK1 = C[25];
    //            double AK2 = C[26];
    //            double AQ0 = C[27];
    //            double AQ1 = C[28];
    //            double AQ2 = C[29];
    //            double AZ = C[30];
    //            double AP0 = C[31];
    //            double AP1 = C[32];
    //            double AP2 = C[33];
    //            double AF0 = C[34];
    //            double AF1 = C[35];
    //            double AF2 = C[36];
    //            double AF3 = C[37];
    //            double AF4 = C[38];
    //            double ALAM = C[39];
    //            double AN0 = C[40];
    //            double OH = C[44];
    //            double AMU = C[45];
    //            double AKF2 = C[46];
    //            double AKF4 = C[47];
    //            double AKP2 = C[48];
    //            double AKP4 = C[49];
    //            double CE = (1.0e0 - BS / AB) / AE;
    //            double SE = Math.Sqrt(1.0e0 - CE * CE);
    //            if (VS < 0.0e0)
    //                SE = -SE;
    //            double EA = DATAN2(SE, CE);
    //            double CP = (CE - AX) / (1.0e0 - AX * CE);
    //            double SP = Math.Sqrt(1.0e0 - CP * CP);
    //            if (VS < 0.0e0)
    //                SP = -SP;
    //            double EP = DATAN2(SP, CP);
    //            double SR = (BT - AG) / (ASZ - BT * AT);
    //            double CR = Math.Sqrt(1.0e0 - SR * SR);
    //            if (VT < 0.0e0)
    //                CR = -CR;
    //            double SF = CR;
    //            double CF = -SR;
    //            double EF = DATAN2(SF, CF);
    //            double SPP = 2.0e0 * SP * CP;
    //            double CPP = CP * CP - SP * SP;
    //            double SPR = 2.0e0 * SPP * CPP;
    //            double SFF = 2.0e0 * SF * CF;
    //            double CFF = CF * CF - SF * SF;
    //            double SFR = 2.0e0 * SFF * CFF;
    //            double AM0 = EA - AZ * SE - ALAM * EP + AP1 * SP + AP2 * SPP + AF1 * SF - AF2 * SFF - AF3 * (SF * CFF + CF * SFF) + AF4 * SFR;
    //            double AMR = AM0 / (1 - ALAM);
    //            double PUDL = EP - AMR;
    //            double AO0 = EF - EP - OH * PUDL - AKP2 * SPP - AKP4 * SPR + AKF2 * SFF - AKF4 * SFR;
    //            double AOR = AO0 - 2.0 * Math.PI;
    //            R1 = -AC * CF + AQ;
    //            double BW = DATAN2(X2, X1) - DATAN2(R1, SF);
    //            double AU0 = BW - (2.0 * Math.PI) - AMU * PUDL - AC1 * SP - AC2 * SPP - AC3 * (SPP * CP + CPP * SP) - AC4 * SPR - AQ1 * SF + AQ2 * SFF;
    //            double AUR = AU0 + 2.0 * Math.PI;
    //            double AM = MyMath.RadiansToDegrees * AMR; // mean anomaly in degree
    //            if (AM < 0.0) AM = 360.0 + AM;
    //            if (AM > 360.0) AM = AM - 360.0;
    //            double AO = MyMath.RadiansToDegrees * AOR; // argument of perigei in degree
    //            if (AO < 0.0) AO = 360.0 + AO;
    //            if (AO > 360.0) AO = AO - 360.0;
    //            double AU = MyMath.RadiansToDegrees * AUR; // the ascending node in degree
    //            if (AU < 0.0) AU = 360.0 + AU;
    //            if (AU > 360.0) AU = AU - 360.0;
    //            double AI = MyMath.RadiansToDegrees * DATAN2(ASZ, AC);
    //            double ANOB = (AN0 / (1 - ALAM)) * (RADGRA / 360.0e0) * 8.64e4; // mean motion rev. per day
    //            ElemR.t = t;
    //            ElemR.ao = AO;   // argument of perigei in degree
    //            ElemR.au = AU;   // the ascending node in degree
    //            ElemR.ai = AI;   // inclination in degree
    //            ElemR.ae = AE;
    //            ElemR.am = AM;   // mean anomaly in degree
    //            ElemR.an = ANOB; // mean motion in revolution per day
    //        }

    //        public void ElemDefault(ref TElemRec el)
    //        { // simple default elements to record el
    //            el.satnord = 1100001;
    //            el.satnuml = 1100001;
    //            el.satname = "unknown";
    //            el.stdmag = 99.0;
    //            el.t = Consts.JD2000; // const UnConTyp
    //            el.ao = 10.0;  // argument of perigei degree
    //            el.au = 80.0;  // ascending node degree
    //            el.ai = 82.0;  // inclination degree
    //            el.ae = 0.01;  // eccentricity
    //            el.am = 1.00;  // mean anomaly degree
    //            el.an = 13.5;  // mean motion in revolution per day
    //            el.dn = 0.00;  // change of mean motion
    //        }

    //    }


    //    public class UnitShad
    //    {

    //        public byte ToGetShadowCase(double tc, vec3 xp)
    //        // only for satellite  after call proc ClcThreeRotMatr for RotMatr
    //        // this function is called by SatellitesAmongStars UnForSat
    //        { // xp[1..3] is satellite position refer to the centre of the Earth     
    //            dvec3 xs;
    //            UnForPos.PosGeoPlanet(PUnDE200.NSun, tc, out xs); // UnForPos
    //            if (!PUnDE200.BooExistPos)
    //                return; // no ephemeris
    //            dvec3 xa = Global.RotMatr * xs; // from fixed to true equator UnForFun
    //            dvec3 ps = xa - xp; // the Sun refer to the satellite
    //            dvec3 pe = -xp; // the centre of the Earth refer to the satellite

    //            double rs = SimpleProcedures.VectorModul(ps); // range from satellite to the Sun
    //            double re = SimpleProcedures.VectorModul(pe); // range from satellite to the centre of the Earth
    //            double cb = (ps[1] * pe[1] + ps[2] * pe[2] + ps[3] * pe[3]) / (rs * re); // cos(beta)
    //            if (cb < 0.0) return 0; // no shadow ToGetShadowCase = 0
    //            double sb = Math.Sqrt(1 - cb * cb); // sin(beta)
    //            double rperp = re * sb; // length of perpendicular in km
    //            double sperp = re * cb; // range from satellite to perpendicular
    //            double rr = rs - sperp;  // range from the Sun to perpendicular
    //            double qq = sperp / rr;   // the ratio
    //            xs = (xp + qq * xa) / (1 + qq); // vector perpendicular
    //            double ze = Consts.GeoR0 * (1 - Consts.ParmA * Math.Pow(xs[3] / rperp, 2)); // const UnConTyp
    //            double zs = PUnDE200.RadiusOfPlanet[PUnDE200.NSun] * sperp / rs; // projection the Sun radius
    //            if (rperp < (ze - zs))
    //                // full shadow
    //                return 2;
    //            else
    //                if (rperp < (ze + zs))
    //                // semishadow case
    //                return 1;
    //            else
    //                return 0;
    //        }

    //        public byte ToMapShadowCase(dvec3 xp, dvec3 xs) // satellite the Sun
    //                                                        // refer to the Earth in rotating system
    //        {
    //            dvec3 ps = xs - xp; // the Sun refer to the satellite
    //            dvec3 pe = -xp; // the centre of the Earth refer to the satellite

    //            double rs = SimpleProcedures.VectorModul(ps); // range from satellite to the Sun
    //            double re = SimpleProcedures.VectorModul(pe); // range from satellite to the centre of the Earth
    //            double cb = (ps[1] * pe[1] + ps[2] * pe[2] + ps[3] * pe[3]) / (rs * re);
    //            if (cb < 0.0) return 0; // no shadow ToGetShadowCase = 0
    //            double sb = Math.Sqrt(1 - cb * cb); // sin(beta)
    //            double rperp = re * sb; // length of perpendicular in km
    //            double sperp = re * cb; // range from satellite to perpendicular
    //            double rr = rs - sperp;  // range from the Sun to perpendicular
    //            double qq = sperp / rr;   // the ratio

    //            dvec3 x = (xp + qq * xs) / (1 + qq); // vector perpendicular

    //            double ze = Consts.GeoR0 * (1 - Consts.ParmA * Math.Pow(x[3] / rperp, 2)); // const UnConTyp
    //            double zs = PUnDE200.RadiusOfPlanet[PUnDE200.NSun] * sperp / rs; // projection the Sun radius
    //            if (rperp < (ze - zs))
    //                // full shadow
    //                return 2;
    //            else
    //                if (rperp < (ze + zs))
    //                // semishadow case
    //                return 1;
    //            else
    //                return 0;
    //        }


    //        public byte ToGetMoonEclipse(dvec3 pe)
    //        // pe[..]  the Moon position in true equator refer to the Earth centre
    //        {
    //            dvec3 pa;
    //            UnForPos.PosGeoPlanet(PUnDE200.NSun, Global.JulianDate, pa); // procedure from UnForPos
    //            if (!PUnDE200.BooExistPos) return; // no planet ephemeris strange case
    //            dvec3 ps = Global.RotMatr * pa; // to true equator UnForFun the Sun  
    //            double rs = SimpleProcedures.VectorModul(ps); // range from the Earth to the Sun
    //            double re = SimpleProcedures.VectorModul(pe); // range from the Earth to the Moon
    //            double cb = (ps[1] * pe[1] + ps[2] * pe[2] + ps[3] * pe[3]) / (rs * re); // cos(beta)
    //            if (cb > -0.9988) return 0; // no shadow ToGetMoonEclipse = 0
    //            double r = PUnDE200.RadiusOfPlanet[PUnDE200.NEarth]; // more detail analyse of the condition
    //            double p = PUnDE200.RadiusOfPlanet[PUnDE200.NMoon];  // const from PUnDE200
    //            double s = PUnDE200.RadiusOfPlanet[PUnDE200.NSun];
    //            double q = rs * r / (s - r);
    //            double rf = r * (q - re) / q; // radius of the shadow
    //            double sf = (rf + p) / re;
    //            double cf = -Math.Sqrt(1 - sf * sf); // max cos for shadow
    //            q = rs * r / (s + r);
    //            double rh = r * (q + re) / q; // radius of the semishadow
    //            double sh = (rh + p) / re;
    //            double ch = -Math.Sqrt(1 - sh * sh); // max cos for semishadow
    //            if (cb < cf)
    //                // full shadow
    //                return 2;
    //            else
    //                if (cb < ch)
    //                // semi-shadow case
    //                return 1;
    //            else
    //                return 0;
    //        }

    //        public byte ToGetShadow(byte np)
    //        { // moment of time in global var JulianDate
    //            dvec3 xe; // type UnConTyp
    //            if (np == PUnDE200.NMoon)
    //            // only for the Moon  may be shadow
    //            { // xp the Moon refer to the Earth centre
    //                UnForPos.PosGeoPlanet(np, Global.JulianDate, out xe); // UnForPos the Moon fix equator
    //                if (!PUnDE200.BooExistPos) return; // no planet ephemeris strange case

    //                dvec3 xp = Global.RotMatr * xe;  // th Moon to true equator UnForFun

    //                return ToGetMoonEclipse(xp); // function from UnitShad
    //            }
    //            else
    //                return 0; // no shadow
    //        }

    //    }

    //    public static class ASTROPUZ_ANALIZ
    //    {
    //        public static void SaTopSpher(
    //            double tc, // moment in julian date       
    //            UnCoTVar.TElemRec el, // record for elements UnCoTVar                     
    //            Global.TPlaceCooRec pc, // record for site UnGloVar                       
    //            out int shadow, // shadow case                       
    //            out double az, out double al, out double ro)  // topocentric
    //        {
    //            // azimut altitude in degree range in kilimeter with refraction
    //            vec3 xe; //  geocentric satellite position true equator
    //            vec3 xs; // topocentric satellite position type from UnConTyp
    //                     // in PosTopSat ClcThreeRotMatr is called with value StationPos
    //            GeoTopSat(tc, fmSelEph.SatElemEph, fmSelEph.SiteCooEph, xe, xs); // UnForSat
    //            shadow = ToGetShadowCase(tc, xe);  // UnitShad for geocentric pos
    //            CoordConverter.ClcTopCoorInDegree(xs, out az, out al, out ro); // UnForCoo with refraction correction
    //        }// byte 2 shadow 1 semishadow 0 lightning



    //        public static void GeoTopSat(
    //            double tcur, // satellite topocentric position                   
    //            UnCoTVar.TElemRec elem, // type UnCoTVar record elements                    
    //            Global.TPlaceCooRec posc, // UnGloVar geodet position                    
    //            out vec3 xe,   // sat geocentric true eqautor                    
    //            out vec3 xp)   // sat topocentric horizont pos
    //        { // value for StationPos[1..3] is obtained by proc ClcThreeRotMatr

    //            vec3 xs; // satellite refer to the station  in true equator           
    //            ClcThreeRotMatr(JD2000, tcur, // UnConTyp UnGloVar for StationPos true
    //                  posc, RotMatr, RosMatr, TopMatr); // UnForCoo  equator
    //            SatPosWithLightDelay(tcur, elem, StationPos, xe, xs); // above xe geocentric
    //            MatrMultVector(TopMatr, xs, xp); // to topocentric horizon
    //        }


    //    }



    //    public static class UnForPos // only for Solar system planet
    //    {

    //        //  UnCotVar , // for types
    //        //  UnGloVar , // for current date variable JulianDate PrecMatr MatrNut
    //        //  UnForTim , // for FromUTCtoTT function ToGetTEph
    //        //  PUnDE200 , // for ephemeris
    //        //  UnForRed , // for aberration correction
    //        //  UnitShad , // for byte function ToGetShadowCase
    //        //  UnForFun , // for UMatrMultVect MatrMultVector procedure
    //        //  UnForCoo , // for procedure ClcTopCoorInDegree ClcThreeRotMatr
    //        //  UnitSort , // for procedure ShellMethodForOrdering
    //        //  UnPlaceA , // for RectCoordinates
    //        //  UnPlateC , // for IdealCoordinates
    //        //  UnGraAll , // for procedure FigurePlanet
    //        //  UnButIni ; // for SpecButt[21] and HintButt[21] day twilight night

    //        //var // PUnD200 UnCoTVar
    //        //  TopArray  : Array[1..NumberOfPlanets] of TObjectTopRec;
    //        //  IntOrder  : TIntegerNumber ; // UnitSort
    //        //  RealSort  : TRealNumber ;    // UnitSort

    //        // after ClcThreeRotMatr from unit UnForCoo
    //        // there are matrix to transform to true equator for current date

    //        public static double MagnitudeValue(byte n, double ro, double rop, double phase)
    //        {
    //            double g:= 99.0;
    //            double f:= RadGra * phase; { in degree UnConTyp}
    //   case  n of
    //            NMercury: g:= -0.38 + 5 * Log10(ro * rop) + 0.0380 * f
    //                  - 0.000273 * f * f + 0.000002 * f * f * f;
    //            NVenus: g:= -4.41 + 5 * Log10(ro * rop) + 0.01314 * f
    //                        + 0.0000004351 * f * f * f;
    //            NMars: g:= -1.51 + 5 * Log10(ro * rop) + 0.01486 * f;
    //            NJupiter: g:= -9.40 + 5 * Log10(ro * rop);
    //            NSaturn: g:= -8.88 + 5 * Log10(ro * rop);
    //            NUranus: g:= -7.19 + 5 * Log10(ro * rop);
    //            NNeptune: g:= -6.87 + 5 * Log10(ro * rop);
    //            NPluto: g:= -1.00 + 5 * Log10(ro * rop);
    //            end; //{ case n }
    //            return g;
    //        }

    //        public static double MagSaturnRing()
    //        //Var
    //        //  i         : Byte ;
    //        //  rotm      : TMatr33 ; // UnConTyp
    //        //  pq,pp     : TVect3 ;
    //        //  pe,ps,pt  : TVect3 ;
    //        //  ap,dp,rp  : Extended ;
    //        //  ae,de,re  : Extended ;
    //        //  az,ds,rs  : Extended ;
    //        //  dt,sde    : Extended ;
    //        { // PrecMatr and MatrNut from UnGloVar are assigned by ToClcThreeRot...
    //            dt:= (JulianDate - JD2000) / 36525.00; // difference in century
    //            ap:= 40.58 - 0.036 * dt;  // north pole of the Saturn in J2000.0 equator
    //            dp:= 83.54 - 0.004 * dt;  // in degree right ascension declination
    //            rp:= 1.0; // unit distance  value for PrecMatr exists
    //            DescFromSpherCoor(ap, dp, rp, pq); // UnForCoo pole descart position
    //            MatrMultVector(PrecMatr, pq, pp); //  Saturn north pole to equator of date
    //            ClcSpherCoorInDegree(pp, ap, dp, rp); // descart position in equator of date
    //            RotMatrFromPole(ap, dp, rotm); // UnForFun rotation matrix to Saturn equator
    //            pq:= PosDim[NSaturn]; // topocentric descart position of Saturn
    //            UMatrMultVect(TopMatr, pq, pt); // topocentric but true equator
    //            UMatrMultVect(MatrNut, pt, pe); // the same but equator of date for Saturn
    //            pq:= PosDim[NSun]; // topocentric descart position of the Sun
    //            UMatrMultVect(TopMatr, pq, pt); // topocentric but true equator
    //            UMatrMultVect(MatrNut, pt, ps); // the same but equator of date for the Sun
    //            For i:= 1 To 3 Do
    //               Begin // change from station to the Saturn in equator of date
    //       ps[i]:= -pe[i] + ps[i];
    //            pe[i]:= -pe[i];
    //            End;
    //            MatrMultVector(rotm, pe, pq); // to the Saturn equator rotation matrix
    //            ClcSpherCoorInDegree(pq, ae, de, re); // the Earth on the Saturn equator
    //            MatrMultVector(rotm, ps, pq); // to the Saturn equator rotation matrix
    //            ClcSpherCoorInDegree(pq, az, ds, rs); // the Sun on the Saturn equator
    //            sde:= Sin(GraRad * de);
    //            MagSaturnRing:= +0.044 * Abs(az - ae) - 2.60 * Abs(sde) + 1.25 * Sqr(sde);
    //        }

    //        // { np number of planet
    //        //   PosP position planet refer to our point
    //        // PosS  position the Sun refer to our point }

    //        public static void ClcPlanetMagnitude(byte np, dvec3 PosP, dvec3 PosS, out double vmag, out double phas)
    //        //var
    //        //  i  : Byte ;
    //        //  a,b,c  : Extended ;
    //        //  s,f    : Extended ;
    //        //  ra,rb  : TVect3 ;
    //        {
    //            if (np = NEarth) or(np = NSun)  then
    //         begin
    //              vmag:= 99;
    //            phas:= 1.0;
    //            Exit;
    //            end;
    //            for i:= 1 to 3 do
    //                    begin
    //                      ra[i]:= -PosP[i]; { the point from the planet }
    //            rb[i]:= +PosS[i] - PosP[i]; { the Sun in topocentric }
    //            end;
    //            a:= Sqrt(Sqr(ra[1]) + Sqr(ra[2]) + Sqr(ra[3]));
    //            b:= Sqrt(Sqr(rb[1]) + Sqr(rb[2]) + Sqr(rb[3]));
    //            c:= (ra[1] * rb[1] + ra[2] * rb[2] + ra[3] * rb[3]) / (a * b); { cos(Phase) }
    //            phas:= 0.5 * (1 + c);
    //            if  np <> NMoon  then
    //              begin
    //              s:= Sqrt(1 - Sqr(c));
    //            f:= DATan2(s, c); // function from UnForFun
    //            a:= a / AstrUnit;
    //            b:= b / AstrUnit;
    //            vmag:= MagnitudeValue(np, a, b, f);
    //            if  np = NSaturn then
    //              vmag:= vmag + MagSaturnRing;
    //            end
    //          else
    //     vmag:= 99; // for the Moon
    //        }

    //        public static void ForSimplePos(byte np,    // number of planet
    //       double tb,// moment in TDB scale
    //                                dvec3 pf, dvec3 pb, // point pf to the Earth
    //                                 out dvec3 pp) // pb to barycenter
    //                                               //var // in pp[..] position refer to station or other object with pf[.]
    //                                               //  i  : Byte ;
    //                                               //  v  : TVect3 ;
    //        {
    //            ClcPosVel(np, tb, pp, v); // the planet to Solar system barycenter
    //            for i:= 1 to 3 do // position of the planet in fixed equator
    //                    if  np <> NMoon // NMoon ClcPosVel from PUnDE200
    //       then // for all planets and the Sun
    //         pp[i]:= pp[i] - pb[i] // refer to the current point
    //       else // only for the Moon refer to the centre of the Earth
    //         pp[i]:= pp[i] - pf[i]; // refer to the current point
    //        }

    //        public static void PosReferToPoint(byte np,// number of planet
    //       double tb, // moment in TDB scale
    //                                   dvec3 pf, dvec3 pb, // point pf to the Earth
    //                                    out dvec3 pp) // pb to barycenter
    //                                                  //Var // pp[1..3] position of the planet refer to the point in fixed equator
    //                                                  //  iter  : Byte ;
    //                                                  //  rs,dt,tcur  : Extended ;
    //        {
    //            ForSimplePos(np, tb, pf, pb, pp); // here above
    //            For iter:= 1 To 3 Do // to get position of the planet
    //               Begin // with light delay by simple iteration
    //       rs:= VectorModul(pp); // range from point to the planet in km
    //            dt:= (rs / VelOfLight) / 86400; // light delay in part of day UnConTyp
    //            tcur:= tb - dt; // moment with light delay
    //            ForSimplePos(np, tcur, pf, pb, pp); // here above
    //            End; // in fixed equator
    //            ForSimplePos(np, tcur, pf, pb, pp); // here above
    //        }

    //        public static char DayOrNightNow() // current date in JulianDate
    //                                           //Var
    //                                           //  i  : Byte ;
    //                                           //  azt,alt,ro,rs  : Extended ;
    //                                           //  TinTDT,TinTDB,DeltaTA  : Extended ;
    //                                           //  PosE,PosS,Vel,pof,pob  : TVect3 ;
    //                                           //  ch  : Char ;
    //        { // current station position in km StationPos in true equator system
    //            DayOrNightNow:= 'N'; // may be no ephemeris file
    //            FromUTCtoTT(JulianDate, TinTDT, DeltaTA);  // unit UnForTim
    //            TinTDB:= ToGetTEph(TinTDT); // function from unit UnForTim
    //            ClcEphEarth(TinTDB, PosE, Vel); // the Earth to Solar system barycentre
    //            If NOT BooExistPos Then  Exit; // boolean var from unit PUnDE200
    //            ClcThreeRotMatr(JD2000, JulianDate, PlaceCoor, RotMatr, RosMatr, TopMatr);
    //            UMatrMultVect(RotMatr, StationPos, pof); // station to fix equator
    //            For i:= 1 To 3 Do // vector for station position in fixed equator in km
    //               pob[i]:= PosE[i] + pof[i]; // refer to Solar system barycentre
    //            PosReferToPoint(NSun, TinTDB, pof, pob, PosS);
    //            MatrMultVector(RotMatr, PosS, PosE); // from fixed to true equator UnForFun
    //            MatrMultVector(TopMatr, PosE, PosS); // to topocentric horizontal system
    //            ClcTopCoorInDegree(PosS, azt, alt, ro); // UnForCoo azimut altitude range
    //            rs:= RadGra * RadiusOfPlanet[NSun] / ro; // half Sun from station in degree
    //            If alt > -rs // the Sun height above horizon with the refraction
    //    Then ch:= 'D' // correction and with the Solar radius
    //     Else         // now is the day the Sun above horizon
    //       If  alt > -6.0   // in degree
    //         Then ch:= 'T'  // astronomical twilight
    //         Else ch:= 'N'; // night for faint stars
    //            ForSunHightButton(ch, alt); // procedure from unit UnButIni
    //            DayOrNightNow:= ch;
    //        }

    //        public static char chDayOrNight(double tt)
    //        //Var // it is possible to call this function after proc ClcThreeRotMatr
    //        //  i  : Byte ;
    //        //  azt,alt,ro,rs  : Extended ;
    //        //  TinTDT,TinTDB,DeltaTA  : Extended ;
    //        //  PosE,PosS,Vel,pof,pob  : TVect3 ;
    //        //  ch  : Char ;
    //        { // current station position in km StationPos in true equator system
    //            chDayOrNight:= 'N'; // may be no ephemeris file
    //            FromUTCtoTT(tt, TinTDT, DeltaTA);  // unit UnForTim
    //            TinTDB:= ToGetTEph(TinTDT); // function from unit UnForTim
    //            ClcEphEarth(TinTDB, PosE, Vel); // the Earth to Solar system barycentre
    //            If NOT BooExistPos Then  Exit; // boolean var from unit PUnDE200
    //            UMatrMultVect(RotMatr, StationPos, pof); // station to fix equator
    //            For i:= 1 To 3 Do // vector for station position in fixed equator in km
    //               pob[i]:= PosE[i] + pof[i]; // refer to Solar system barycentre
    //            PosReferToPoint(NSun, TinTDB, pof, pob, PosS);
    //            MatrMultVector(RotMatr, PosS, PosE); // from fixed to true equator UnForFun
    //            MatrMultVector(TopMatr, PosE, PosS); // to topocentric horizontal system
    //            ClcTopCoorInDegree(PosS, azt, alt, ro); // UnForCoo azimut altitude range
    //            rs:= RadGra * RadiusOfPlanet[NSun] / ro; // half Sun from station in degree
    //            If alt > -rs // the Sun height above horizon with the refraction
    //    Then ch:= 'D' // correction and with the Solar radius
    //     Else         // now is the day the Sun above horizon
    //       If  alt > -6.0   // in degree
    //         Then ch:= 'T'  // astronomical twilight
    //         Else ch:= 'N'; // night for faint stars
    //            chDayOrNight:= ch;
    //        }

    //        public static string stDayOrNight(double tt, double aa, double dd)
    //        //Var // tt current moment aa longitude dd latitude of the point on the Earth
    //        //  i  : Byte ;
    //        //  azt,alt,ro,rs  : Extended ;
    //        //  TinTDT,TinTDB,DeltaTA  : Extended ;
    //        //  PosE,PosS,Vel,pof,pob  : TVect3 ;
    //        //  pl  : TPlaceCooRec ;
    //        //  st  : ShortString ;
    //        { // current station position in km StationPos in true equator system
    //            stDayOrNight:= '  ночь  '; // may be no ephemeris file
    //            pl.num:= 0;
    //            pl.name:= '';
    //            pl.f:= dd;  // latitude in degree
    //            pl.l:= aa;  // logitude in degree
    //            pl.h:= 0.0; // height in meter
    //            with pl  do GeoCartFromSpher(f, l, h, x, y, z); // proc from UnForCoo

    //            ClcThreeRotMatr(JD2000, tt, pl, RotMatr, RosMatr, TopMatr);
    //            FromUTCtoTT(tt, TinTDT, DeltaTA);  // unit UnForTim
    //            TinTDB:= ToGetTEph(TinTDT); // function from unit UnForTim
    //            ClcEphEarth(TinTDB, PosE, Vel); // the Earth to Solar system barycentre
    //            If NOT BooExistPos Then  Exit; // boolean var from unit PUnDE200
    //            UMatrMultVect(RotMatr, StationPos, pof); // station to fix equator
    //            For i:= 1 To 3 Do // vector for station position in fixed equator in km
    //               pob[i]:= PosE[i] + pof[i]; // refer to Solar system barycentre
    //            PosReferToPoint(NSun, TinTDB, pof, pob, PosS);
    //            MatrMultVector(RotMatr, PosS, PosE); // from fixed to true equator UnForFun
    //            MatrMultVector(TopMatr, PosE, PosS); // to topocentric horizontal system
    //            ClcTopCoorInDegree(PosS, azt, alt, ro); // UnForCoo azimut altitude range
    //            rs:= RadGra * RadiusOfPlanet[NSun] / ro; // half Sun from station in degree
    //            If alt > -rs // the Sun height above horizon with the refraction
    //    Then st:= '  день  ' // correction and with the Solar radius
    //     Else         // now is the day the Sun above horizon
    //       If  alt > -6.0   // in degree
    //         Then st:= 'сумерки '  // astronomical twilight
    //         Else st:= '  ночь  '; // night for faint stars
    //            stDayOrNight:= st;
    //        }

    //        public static void PlanetsToHeapWithFigure(int numc)
    //        //Var
    //        //  nc  : Integer ;
    //        //   np  : Integer ;
    //        {
    //            nc:= 0;
    //            Repeat
    //              nc:= nc + 1;
    //            np:= IntOrder[nc];
    //            ObjectTopCur:= TopArray[np];
    //            Inc(NumObjectInHeap);
    //            ObjectTopHeap ^[NumObjectInHeap]:= ObjectTopCur;
    //            With ObjectTopCur  do
    //                If booscreen and BooPosInRect(xpos, ypos)
    //       Then FigurePlanet; // unit UnGraAll
    //            Until nc = numc;
    //        }

    //        public static void PlanetForOrder(byte np, double az, double al, double ro, double vmag, double phasep, byte ishad, out int numc)
    //        //Var
    //        //  x, y  : Integer ;
    //        //  ksi,eta  : Extended ;
    //        //  top  : TVect3 ; // UnConTyp topocentric horizon descart
    //        //  bos  : Boolean ; // our planet may be in the other hemisphere
    //        {
    //            top:= PosDim[np]; // topocentric position of current planet
    //            { PosHorizoC UnGloVar topo horizon vector of the centre
    //              our planet may be out of our hemisphere
    //     ScalarMunt simple cosinus between two vectors
    //              if cosinus > 0.5 then planet is in our hemishere
    //              if cosinus > 0.5 then planet may be in our field of view }
    //            bos:= (ScalarMunt(top, PosHorizoC) > 0.5); // UnForFun
    //            if  bos        // planet is in our hemishere
    //     then         // this planet is in our hemisphere
    //       begin      // this planet may be in our field of view
    //         IdealCoordinates(az, al, ksi, eta); // from UnPlateC
    //            RectCoordinates(ksi, eta, x, y);    // from UnPlaceA
    //            end // position projection to the plate
    //     else     // this planet is out our hemisphere
    //       begin  // this palnet is out our field if view
    //         x:= -30000; // default large minus position
    //            y:= -30000; // default large minus position
    //            end;   // to exlude this planet
    //            ObjectTopCur.nall:= np; // number
    //            ObjectTopCur.chps:= 'P'; // there is planet
    //            ObjectTopCur.name:= PlanetNameR[np]; // PUnDE200
    //            ObjectTopCur.azt:= az; // current azimuth
    //            ObjectTopCur.alt:= al; // current altitude
    //            ObjectTopCur.rot:= ro; // range default for star
    //            ObjectTopCur.Mag:= vmag; // magnitude
    //            ObjectTopCur.phase:= phasep; // for planet
    //            ObjectTopCur.IndexShadow:= ishad; // default
    //            If al > 0.0                    // altitude above horizon
    //     Then
    //              ObjectTopCur.boovis:= True   // visibility
    //     Else
    //              ObjectTopCur.boovis:= False; // no visibility
    //            ObjectTopCur.booscreen:= bos;    // screen condition
    //            ObjectTopCur.xpos:= x; // x on screen
    //            ObjectTopCur.ypos:= y; // y on screen
    //            Inc(numc);
    //            TopArray[numc]:= ObjectTopCur;
    //            IntOrder[numc]:= numc; // initial order in array
    //            RealSort[numc]:= ObjectTopCur.rot; // to order with range in km
    //        }

    //        public static void PlanetsToScreen() // current date in JulianDate
    //                                             //Var                         // proc is called by ViewPlac
    //                                             //  i  : Byte ;
    //                                             //   np  : Byte ;
    //                                             //   numc  : Integer ;
    //                                             //   vmag  : Single ;
    //                                             //   ishad  : Byte ;
    //                                             //   azt,alt,ro,phase  : Extended ;
    //                                             //   TinTDT,TinTDB,DeltaTA  : Extended ;
    //                                             //   PosE,PosP,PosS,Vel,pof,pob  : TVect3 ;
    //        { // current station position in km StationPos in true equator system
    //            FromUTCtoTT(JulianDate, TinTDT, DeltaTA);  // unit UnForTim
    //            TinTDB= ToGetTEph(TinTDT); // function from unit UnForTim
    //            ClcEphEarth(TinTDB, PosE, Vel); // the Earth to Solar system barycentre
    //            if( ! BooExistPos )  Exit; // boolean var from unit PUnDE200
    //            UMatrMultVect(RotMatr, StationPos, pof); // station to fix equator
    //            for( i:= 1 To 3 ) // vector for station position in fixed equator in km
    //               pob[i]= PosE[i] + pof[i]; // refer to Solar system barycentre
    //            PosReferToPoint(NSun, TinTDB, pof, pob, PosS); // the Sun refer to point
    //            MatrMultVector(RotMatr, PosS, Vel); // from fixed to true equator
    //            MatrMultVector(TopMatr, Vel, PosS); // to topocentric horizont the Sun
    //            numc= 0; // inital nullo before order all planets with range
    //            For np= NMercury To NSun Do
    //               if (np != NEarth)  // pof point refer to the Earth
    //            {  // pob point refer to the Solar system barycenter
    //                PosReferToPoint(np, TinTDB, pof, pob, PosP); // PosP refer to point
    //                ForAberrationCorrection(PosP); // UnForRed correction in fix equator
    //                MatrMultVector(RotMatr, PosP, Vel); // from fixed to true equator
    //                MatrMultVector(TopMatr, Vel, PosP); // to topocentric horizont
    //                PosDim[np]= PosP; // topocentric position of the planet to array
    //                ClcPlanetMagnitude(np, PosP, PosS, vmag, phase);
    //                ClcTopCoorInDegree(PosP, azt, alt, ro); // UnForCoo
    //                ishad= ToGetShadow(np); // UnitShad
    //                PlanetForOrder(np, azt, alt, ro, vmag, phase, ishad, numc);
    //            }
    //            for( i:= 1 To 3 ) PosDim[NEarth][i]:=0.0; // array PosDim from PUnDE200
    //   ShellMethodForOrdering(numc, RealSort, IntOrder); // UnitSort with range
    //   PlanetsToHeapWithFigure(numc);
    //    }

    //    public static void PosTopPlanet(byte np, double tcur, out double azt, out double alt)
    //    //Var
    //    //  i  : Byte ;
    //    //  ro,TinTDT,TinTDB,dtmp  : Extended ;
    //    //  PosE,PosP,tmp,pof,pob  : TVect3 ;
    //    {
    //        ClcThreeRotMatr(JD2000, tcur, // UnConTyp UnGloVar for StationPos true
    //                        PlaceCoor, RotMatr, RosMatr, TopMatr); // UnForCoo  equator
    //        ToObtainAberrationParm(tcur); // UnForRed
    //        FromUTCtoTT(tcur, TinTDT, dtmp);  // unit UnForTim
    //        TinTDB:= ToGetTEph(TinTDT); // function from unit UnForTim
    //        ClcEphEarth(TinTDB, PosE, tmp); // the Earth to Solar system barycentre
    //        If NOT BooExistPos Then  Exit; // boolean var from unit PUnDE200
    //        UMatrMultVect(RotMatr, StationPos, pof); // station to the Earth fix equator
    //        For i:= 1 To 3 Do // vector for station position in fixed equator in km
    //           pob[i]:= PosE[i] + pof[i]; // pob refer to Solar system barycentre
    //        PosReferToPoint(np, TinTDB, pof, pob, PosP); // PosP refer to station
    //        ForAberrationCorrection(PosP); // UnForRed correction in fix equator
    //        MatrMultVector(RotMatr, PosP, tmp); // from fixed to true equator UnForFun
    //        MatrMultVector(TopMatr, tmp, PosP); // to topocentric horizont UnForFun
    //        ClcTopCoorInDegree(PosP, azt, alt, ro); // UnForCoo
    //    }

    //    public static void PosTopSop(byte np,// number of Solar system object
    //   double tcur,// current moment
    //                         TPlaceCooRec posc, // station geodetic position
    //                          out dvec3 ptop)// in topocentric
    //                                         //Var
    //                                         //  i  : Byte ;
    //                                         //  TinTDT,TinTDB,dtmp  : Extended ;
    //                                         //  pose,posp,tmp,pof,pob  : TVect3 ;
    //    { // StationPos[..] from ClcThreeTorMatr with posc
    //        ClcThreeRotMatr(JD2000, tcur, // UnConTyp UnGloVar for StationPos true
    //                        posc, RotMatr, RosMatr, TopMatr); // UnForCoo  equator
    //        ToObtainAberrationParm(tcur); // UnForRed
    //        FromUTCtoTT(tcur, TinTDT, dtmp);  // unit UnForTim
    //        TinTDB:= ToGetTEph(TinTDT); // function from unit UnForTim
    //        ClcEphEarth(TinTDB, pose, tmp); // the Earth to Solar system barycentre
    //        If NOT BooExistPos Then  Exit; // boolean var from unit PUnDE200
    //        UMatrMultVect(RotMatr, StationPos, pof); // station to the Earth fix equator
    //        For i:= 1 To 3 Do // vector for station position in fixed equator in km
    //           pob[i]:= pose[i] + pof[i]; // pob refer to Solar system barycentre
    //        PosReferToPoint(np, TinTDB, pof, pob, posp); // PosP refer to station
    //        ForAberrationCorrection(posp); // UnForRed correction in fix equator
    //        MatrMultVector(RotMatr, posp, tmp); // from fixed to true equator UnForFun
    //        MatrMultVector(TopMatr, tmp, ptop); // to topocentric horizont UnForFun
    //    }

    //    public static void PosGeoPlanet(int np, double tc, out dvec3 xp)
    //    //var // position of the planet refer to the centre of the Earth with delay
    //    //  ic, it  : Byte ;
    //    //  tt,tb  : Extended ;
    //    //  rr,dc  : Extended ;
    //    //  xe,xg  : TVect3 ;
    //    //  xs,vs  : TVect3 ;
    //    {
    //        FromUTCtoTT(tc, tt, tb);  // unit UnForTim tt scale TDT
    //        tb:= ToGetTEph(tt); // function from unit UnForTim  tb scale TDB
    //        if  np <> NMoon
    //          then
    //          ClcEphEarth(tb, xe, vs) // PUnDE200 the Earth refer to Solar barycentre
    //    else
    //      for ic:= 1 to 3 do xe[ic]:= 0.0; // for the Moon refer to the Earth
    //        if  not BooExistPos then Exit; // global boolean var from PUnDE200
    //        dc:= tb; // for iteration to obtain light delay
    //        ClcPosVel(np, dc, xs, vs); // for the planet or the Sun from PUnDE200
    //        for it:= 1 to 3 do // xs[..] the planet refer to Solar system barycentre
    //                begin // the Sun refer to the centre of the Earth
    //      for ic:= 1 to 3 do xg[ic]:= xs[ic] - xe[ic]; // geocentric for the planet
    //        rr:= VectorModul(xg); // function from UnForFun
    //        dc:= tb - (rr / VelOfLight) / 86400; // time delay const UnConTyp
    //        ClcPosVel(np, dc, xs, vs); // the planet or the Sun position with delay
    //        end;
    //        for ic:= 1 to 3 do xp[ic]:= xs[ic] - xe[ic]; // fix equator geocentric the planet
    //    }

    //    public static bool CentreWithFixPlanet() // called from UnitFixO
    //                                             //Var // to obtain AzimutC AltitudeC centre of the screen in horizon
    //                                             //  tmin, tmax, tcur, step, azt, alt  : Extended ;
    //    {
    //        CentreWithFixPlanet:= False; // default
    //        If(PlanetNumFix < NMercury) or(PlanetNumFix > NSun)
    //        Then  // it is strange number
    //       Exit; // number of planet may be within NMercuty..NSun
    //        if  CharForView = 'L' // 'L' look option without visibility
    //     then                // to  look around from the satellite
    //       begin             // full sky above anfd lower horizon
    //         CentreWithFixPlanet:= True; // well on current date
    //        PosTopPlanet(PlanetNumFix, // selected planet
    //                     JulianDate,   // current moment
    //                     AzimutC, AltitudeC); // new centre
    //        Exit; // only for look option full sky lower horizon
    //        end;
    //        step:= 0.5 / 24.0; // for step in day to find moment of visibility
    //        tmin:= JulianDate;       // start  moment to find
    //        tmax:= tmin + 60.0 * step;   // finish moment to find
    //        tcur:= tmin - step;
    //        Repeat // step by step to find
    //          tcur:= tcur + step; // visibility from a ststion
    //        PosTopPlanet(PlanetNumFix, tcur, azt, alt); // selected planet
    //        If NOT BooExistPos Then  Exit; // boolean var from unit PUnDE200
    //        Until(alt > 0.0) or(tcur > tmax);  // may be visibility
    //        if  alt > 0.0 // altitude more than 0.0 degree for visibility
    //     then
    //       begin     // new centre for field of view
    //         CentreWithFixPlanet:= True; // well
    //        JulianDate:= tcur; // for new date
    //        AzimutC:= azt;     // new centre azimuth
    //        AltitudeC:= alt;   // new centre altitude
    //        If AltitudeC< RinDegC - 1 // but above horizon
    //           Then // all these variables from UnGloVar
    //             AltitudeC:= RinDegC - 1; // above horizon
    //        end;
    //    }

    //    public static void RotatingGeoPos(byte np,  // number of planet
    //        out dvec3 pp, out double fs, out double vs)// lat longitude degree
    //                                                   //Var
    //                                                   //  i, iter   : Byte ;
    //                                                   //  tdt,tdb  : Extended ;
    //                                                   //  tb,rs,dt : Extended ;
    //                                                   //  pe,vp    : TVect3 ;
    //    {
    //        ClcTrueRotMatr(JD2000, JulianDate, RotMatr, RosMatr);  // UnForCoo  equator
    //        FromUTCtoTT(JulianDate, tdt, dt);  // unit UnForTim
    //        tdb:= ToGetTEph(tdt); // function from unit UnForTim
    //   case  np of
    //        NMoon: ClcPosVel(np, tdb, pp, vp); // the Moon refer to the Earth now
    //     else
    // begin
    //      ClcEphEarth(tdb, pe, vp); // the Earth to Solar system barycentre
    //        If NOT BooExistPos Then  Exit; // boolean var from unit PUnDE200
    //        ClcPosVel(np, tdb, pp, vp); // the Sun to Solar system barycenter
    //        For i:= 1 To 3 Do // position of the Sun in fixed equator
    //           pp[i]:= pp[i] - pe[i]; // refer to the centre of the Earth
    //        For iter:= 1 To 3 Do // to get position of the Sun
    //           Begin // with light delay by simple iteration VectorModul UnForFun
    //       rs:= VectorModul(pp); // range from point to the planet in km
    //        dt:= (rs / VelOfLight) / 86400; // light delay in part of day UnConTyp
    //        tb:= tdb - dt; // moment with light delay  PUnDE200
    //        ClcPosVel(np, tb, pp, vp); // the Sun position with light delay
    //        For i:= 1 To 3 Do pp[i]:= pp[i] - pe[i]; // refer to Earth centre
    //        End; // in fixed equator
    //        end;
    //        end; // case  np
    //        MatrMultVector(RotMatr, pp, vp); // from fixed to true equator
    //        MatrMultVector(RosMatr, vp, pp); // to rotating Earth equator UnForFun
    //        ClcSpherCoorInDegree(pp, vs, fs, rs); // long latitude of the Sun UnForCoo
    //    }
    //
    //    }

}
