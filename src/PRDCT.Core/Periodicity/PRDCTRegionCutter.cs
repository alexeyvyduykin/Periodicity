using System;
using System.Collections.Generic;
using GlmSharp;

namespace Periodicity.Core
{
    public class PRDCTRegionCutter
    {
        private enum PolygonDirect
        {
            UpWards = 1,          // /\ 
            RightWards = 2,       // -> 
            DownWards = 3,        // \/ 
            LeftWards = 4         // <- 
        }

        private enum PolygonCutState
        {
            Error = 0,
            True = 1,
            DeleteCurrent = 2,
            DeletePrevious = 3,
            DeleteCurrentAndPrevious = 4
        }

        public PRDCTRegionCutter(Region region)
        {
            this.region = region;
        }

        public List<Tuple<double, double>> Calculation(double latRADCutter)
        {
            if (latRADCutter >= region.Bottom && latRADCutter <= region.Top)
            {
                switch (region.Type)
                {
                    case RegionType.Zone:
                        return new List<Tuple<double, double>> { Tuple.Create(0.0, 2.0 * Math.PI) };
                    case RegionType.Rectangle:
                        return new List<Tuple<double, double>> { Tuple.Create(left(latRADCutter), right(latRADCutter)) };
                    case RegionType.Polygon:
                        return PolygonCutter(latRADCutter);
                }
            }

            return new List<Tuple<double, double>>();
        }

        private double left(double lat)
        {
            if (region.Type == RegionType.Zone || region.Type == RegionType.Rectangle)
            {
                return region.Left;
            }
            else
            {
                dvec2 P1 = new dvec2(0.0, lat);
                dvec2 P2 = new dvec2(2.0 * Math.PI, lat);
                clipCyrusBeck2(ref P1, ref P2);
                return P1.x;
            }
        }

        private double right(double lat)
        {
            if (region.Type == RegionType.Zone || region.Type == RegionType.Rectangle)
            {
                return region.Right;
            }
            else
            {
                dvec2 P1 = new dvec2(0.0, lat);
                dvec2 P2 = new dvec2(2.0 * Math.PI, lat);
                clipCyrusBeck2(ref P1, ref P2);
                return P2.x;
            }
        }

        private bool clipCyrusBeck2(ref dvec2 P1, ref dvec2 P2)
        {
            double tIn = 0.0, tOut = 1.0, tHit;
            dvec2 c = P2 - P1;
            for (int i = 0; i < region.Verts.Count; i++)
            {
                dvec2 tmp = region.Verts[i] - P1;
                double numer = dvec2.Dot(region.Normals[i], tmp);// normals[i].Dot(tmp);
                double denom = dvec2.Dot(region.Normals[i], c);// normals[i].Dot(c);

                if (denom < 0)                           // луч входит
                {
                    tHit = numer / denom;
                    if (tHit > tOut)
                    { return false; }    // досрочный выход
                    else
                    {
                        if (tHit > tIn)
                        { tIn = tHit; }
                    }  // ????? ?????? t
                }
                else if (denom > 0)                   // луч выходит
                {
                    tHit = numer / denom;
                    if (tHit < tIn)
                    { return false; }        // досрочный выход
                    else
                    {
                        if (tHit < tOut)
                        { tOut = tHit; }
                    } // ????? ??????? t
                }
                else
                {                  // denom(знаменатель) равен нулю: луч параллелен
                    int k = (i == region.Verts.Count - 1) ? 0 : i + 1;

                    dvec2 A = region.Verts[i];
                    dvec2 B = region.Verts[k];

                    dvec2 b = B - A;
                    if (MyMath.DoubleEquals(b.x * (P1.y - A.y) - b.y * (P1.x - A.x), 0.0, 0.00001) == true)
                    // порождающие прямые совпадают
                    {
                        double tA = 0.0, tB = 1.0;
                        double tC = (P1.x - A.x) / b.x;
                        double tD = (P2.x - A.x) / b.x;
                        if (((tC < tA) && (tD < tA)) || ((tC > tB) && (tD > tB)))
                        { return false; }

                        if (tC < tA)
                        {
                            tC = tA;
                        }

                        if (tC > tB)
                        {
                            tC = tB;
                        }

                        if (tD < tA)
                        {
                            tD = tA;
                        }

                        if (tD > tB)
                        {
                            tD = tB;
                        }

                        P1 = A + b * tC;
                        P2 = A + b * tD;
                        return true;
                    }
                    if (numer <= 0)
                    {
                        return false;
                    }
                }
            }

            P2 = P1 + c * tOut;
            P1 = P1 + c * tIn;
            return true;
        }

        private bool clipCyrusBeck1(ref dvec2 P1, ref dvec2 P2)
        {
            int i;
            double tIn = 0.0;
            double tOut = 1.0;
            dvec2 D = P2 - P1;

            for (i = 0; i < region.Verts.Count; ++i)
            {
                dvec2 ni = region.Normals[i];
                dvec2 fi = region.Verts[i];
                dvec2 wi = fi - P1;

                double Dck = dvec2.Dot(ni, D);// ni.Dot(D);    // numer
                double Wck = dvec2.Dot(ni, wi);// ni.Dot(wi);   // denom

                if (Dck == 0)
                {
                    if (Wck < 0)
                    {
                        return false;
                    }

                    continue;
                }
                double t = -Wck / Dck;

                if (Dck > 0)
                {
                    if (t > 1)
                    {
                        return false;
                    }

                    tIn = Math.Max(t, tIn);
                }
                else
                {
                    if (t < 0)
                    {
                        return false;
                    }

                    tOut = Math.Max(t, tOut);
                }
            }
            if (tIn > tOut)
            {
                return false;
            }

            P2 = P1 + D * tOut;
            P1 = P1 + D * tIn;
            return true;
        }

        private List<Tuple<double, double>> PolygonCutter(double latRADCutter)
        {
            // все обозначения(b,d,c,...) взяты и с книги OpenGL
            dvec2 A = new dvec2(0.0, latRADCutter);
            dvec2 B = new dvec2(2.0 * Math.PI, latRADCutter);

            var temp_lons = new List<double>();
            var temp_direct = new List<PolygonDirect>();

            dvec2 b = B - A;
            dvec2 b_norm = b.Rotated(Math.PI / 2.0);
            ////////////////////////////////////////////////////////////////////////////////
            for (int i = 0; i < region.Verts.Count; i++)
            {
                dvec2 d = region.Lines[i];
                dvec2 c = region.Verts[i] - A;

                double t_numer = dvec2.Dot(region.Normals[i], c);
                double u_numer = dvec2.Dot(b_norm, c);
                double denom = dvec2.Dot(region.Normals[i], b);

                if (denom != 0)
                {
                    double t_hit = t_numer / denom;          // по отрезку [0, TWOPI]
                    double u_hit = u_numer / denom;          // по стороне полигона

                    // u_hit > 0 !!! учитываем только конец вектора(стороны полигона), включаем только конец
                    if ((t_hit >= 0.0 && t_hit <= 1.0) && (u_hit > 0.0 && u_hit <= 1.0))
                    {
                        temp_lons.Add(2.0 * Math.PI * t_hit);   // lon = TWOPI*t_hit;
                        if (region.Lines[i].y > 0.0)         // upwards
                        {
                            temp_direct.Add(PolygonDirect.UpWards);
                        }
                        else //( lines[i].y < 0.0 )         // downwards
                        {
                            temp_direct.Add(PolygonDirect.DownWards);
                        }
                    }
                }
                else     // denom(знаменатель) равен нулю: луч параллелен
                {
                    d = region.Lines[i];
                    dvec2 C = region.Verts[i];
                    if (MyMath.DoubleEquals(d.x * (A.y - C.y) - d.y * (A.x - C.x), 0.0, 0.00001) == true)
                    // порождающие прямые совпадают
                    {
                        double tA = 0.0, tB = 1.0;
                        double tC = (A.x - C.x) / d.x;
                        double tD = (B.x - C.x) / d.x;
                        if (((tC < tA) && (tD < tA)) || ((tC > tB) && (tD > tB)))
                        { }
                        else
                        {
                            if (tC < tA)
                            {
                                tC = tA;
                            }

                            if (tC > tB)
                            {
                                tC = tB;
                            }

                            if (tD < tA)
                            {
                                tD = tA;
                            }

                            if (tD > tB)
                            {
                                tD = tB;
                            }

                            if (region.Lines[i].x > 0.0)       // rightwards
                            {
                                temp_direct.Add(PolygonDirect.RightWards);
                            }
                            else // lines[i].x < 0.0     // leftwards
                            {
                                temp_direct.Add(PolygonDirect.LeftWards);
                            }

                            dvec2 p2 = C + d * tD;
                            temp_lons.Add(p2.x);
                        }
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////////////////
            PolygonDirect prev, cur, next;
            int cur_length = temp_lons.Count;
            int index_prev, index_next, index_cur = 0;

            while (index_cur < cur_length)
            {
                index_prev = index_cur - 1;
                index_next = index_cur + 1;

                if (index_prev < 0)
                {
                    index_prev += cur_length;
                }

                if (index_next > (cur_length - 1))
                {
                    index_next -= cur_length;
                }

                if (index_cur > (cur_length - 1))
                {
                    index_cur -= cur_length;
                };


                prev = temp_direct[index_prev];
                cur = temp_direct[index_cur];
                next = temp_direct[index_next];

                PolygonCutState mod = comparison(prev, cur, next, cur_length);
                if (mod == PolygonCutState.True)
                {
                    index_cur++;
                }

                if (mod == PolygonCutState.DeleteCurrent)
                {
                    temp_lons.RemoveAt(index_cur);// erase(temp_lons[index_cur]);
                    temp_direct.RemoveAt(index_cur);// erase(temp_direct[index_cur]);
                }
                if (mod == PolygonCutState.DeletePrevious)
                {
                    temp_lons.RemoveAt(index_prev);// erase(temp_lons[index_prev]);
                    temp_direct.RemoveAt(index_prev);// erase(temp_direct[index_prev]);
                    index_cur = 0;
                }
                if (mod == PolygonCutState.DeleteCurrentAndPrevious)
                {
                    temp_lons.RemoveAt(index_cur);// erase(temp_lons[index_cur]);
                    temp_direct.RemoveAt(index_cur);//erase(temp_direct[index_cur]);
                    if (index_cur < index_prev)
                    {
                        index_prev--;
                    }

                    temp_lons.RemoveAt(index_prev);//erase(temp_lons[index_prev]);
                    temp_direct.RemoveAt(index_prev);//erase(temp_direct[index_prev]);
                    index_cur = 0;
                }
                if (mod == PolygonCutState.Error)
                {
                    throw new Exception("PRDCTRegion::PRDCT_CUT() - Неизвестное сочитание prev, cur, next!!!");
                }

                cur_length = (int)temp_lons.Count;
                if (index_cur < 0)
                {
                    index_cur += cur_length;
                }

                if (index_cur < 0)
                {
                    break;
                }
            }
            temp_lons.Sort();
            //sort(temp_lons.begin(), temp_lons.end());

            // необходимы только интервалы, случай касания полигона за одну из вершин не учитывается
            if (cur_length == 1)
            {
                temp_lons.RemoveAt(temp_lons.Count - 1);// pop_back();
            }

            var result = new List<Tuple<double, double>>();

            for (int i = 0; i < temp_lons.Count; i += 2)
            {
                result.Add(Tuple.Create(temp_lons[i], temp_lons[i + 1]));
            }

            return result;
        }

        private PolygonCutState comparison(PolygonDirect prev, PolygonDirect cur, PolygonDirect next, int length)
        {
            int sum;
            int sum2 = 10 * (int)prev + (int)cur;
            int sum3 = 100 * (int)prev + 10 * (int)cur + (int)next;

            if (prev == cur)
            {
                return PolygonCutState.DeletePrevious;
            }

            if (cur == next)
            {
                return PolygonCutState.DeleteCurrent;
            }

            if (length < 3)
            {
                sum = sum2;
            }
            else if ((cur == PolygonDirect.LeftWards) || (cur == PolygonDirect.RightWards))
            {
                sum = sum3;
            }
            else
            {
                sum = sum2;
            }

            switch (sum)
            {
                case 11:
                    return PolygonCutState.DeletePrevious;            // prev удалить
                case 13:
                    return PolygonCutState.True;                   // ничего не предпринимать
                case 31:
                    return PolygonCutState.True;                   // ничего не предпринимать
                case 33:
                    return PolygonCutState.DeletePrevious;            // prev удалить

                case 34:
                    return PolygonCutState.True;                   // ничего не предпринимать
                case 43:
                    return PolygonCutState.True;                   // ничего не предпринимать
                case 12:
                    return PolygonCutState.True;                   // ничего не предпринимать
                case 21:
                    return PolygonCutState.True;                   // ничего не предпринимать

                case 23:
                    return PolygonCutState.DeletePrevious;            // prev удалить ?????

                case 341:
                    return PolygonCutState.DeleteCurrent;            //  cur удалить
                case 143:
                    return PolygonCutState.DeletePrevious;           //  prev удалить
                case 321:
                    return PolygonCutState.DeletePrevious;           //  prev удалить
                case 123:
                    return PolygonCutState.DeleteCurrent;            //  cur удалить

                case 141:
                    return PolygonCutState.DeleteCurrentAndPrevious;   //  cur и prev удалить
                case 343:
                    return PolygonCutState.True;                  //  ничего не предпринимать
                case 121:
                    return PolygonCutState.True;                  //  ничего не предпринимать
                case 323:
                    return PolygonCutState.DeleteCurrentAndPrevious;   //  cur и prev удалить

                default:
                    return PolygonCutState.Error;
            }
        }

        private readonly Region region;
    }
}
