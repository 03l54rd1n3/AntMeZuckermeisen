using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntMe.Deutsch;

namespace AntMe.Player.AntMe.Ameisen
{
    public class KGB : Brain
    {

        private int dir = 0;
        
        public KGB(AntMeKlasse _a)
        {
            a = _a;
        }
        
        public override void Init()
        {
            if (bau == null)
            {
                Storage.ants.Add(this);
                Denke(Storage.ants.Count.ToString() + " - " + Kaste);

                if(Storage.scouting.Count == 0)
                {
                    for(int i = 0; i < 360; i+=45)
                    {
                        Storage.scouting.Add(i, false);
                    }
                }

                for (int i = 0; i < 360; i += 45)
                {
                    if(Storage.scouting[i] == false)
                    {
                        dir = i;
                        Storage.scouting[i] = true;
                        break;
                    }
                }

                GeheZuBau();
                bau = (Bau)Ziel;
                BleibStehen();
                DreheInRichtung(dir);
            }
        }

        #region Fortbewegung

        public void Sprint(Spielobjekt ziel)
        {
            if (ziel != null)
            {
                if (WanzenInSichtweite == 0)
                {
                    GeheZuZiel(ziel);
                    int d = getD(a, ziel);
                    int _a = getA(a, ziel);
                    DreheInRichtung(_a);
                    GeheGeradeaus(d);
                }
            }
            else
            {
                if (ziel == bau)
                {
                    Init();
                    Sprint(ziel);
                }
                else
                {
                    BleibStehen();
                }
            }

        }

        public override void Wartet()
        {
            Init();
            
            if (Reichweite - 20 < getD(a, bau))
            {
                Sprint(bau);
            }
            else
            {
                GeheGeradeaus(Sichtweite);
            }
        }

        public override void WirdMüde()
        {
            
        }
        
        public override void IstGestorben(Todesart todesart)
        {
            Storage.scouting[dir] = false;
            Storage.ants.Remove(this);
            Denke(((Todesart)todesart).ToString());
        }

        public override void Tick()
        {
            //Denke(Kaste);
        }

        #endregion

        #region Nahrung
        
        public override void Sieht(Obst obst)
        {
            
        }

        public override void Sieht(Zucker zucker)
        {
            int n = 0;
            for (int i = 0; i < Storage.zucker.Count; i++)
            {
                if (Storage.zucker[i] == zucker)
                {
                    n = i + 1;
                }
            }
            if (n == 0)
            {
                Storage.zucker.Add(zucker);
                n = Storage.zucker.Count;
            }
            SprüheMarkierung(1000 + n, 1000);
        }

        public override void ZielErreicht(Obst obst)
        {

        }

        public override void ZielErreicht(Zucker zucker)
        {

        }

        #endregion

        #region Kommunikation

        public override void RiechtFreund(Markierung markierung)
        {

        }

        public override void SiehtFreund(Ameise ameise)
        { 
        
        }

        public override void SiehtVerbündeten(Ameise ameise)
        {

        }

        #endregion

        #region Kampf

        public override void SiehtFeind(Ameise ameise)
        {
            int alpha = Richtung;
            int beta = getA(a, ameise);
            int phi = Math.Abs(beta - alpha) % 360;
            int r = phi > 180 ? 360 - phi : phi;
            int sign = (alpha - beta >= 0 && alpha - beta <= 180) || (alpha - beta <= -180 && alpha - beta >= -360) ? 1 : -1;
            r *= sign;
            if (-30 < r && r < 30)
            {
                if (Ziel != null)
                {
                    if (getD(a, Ziel) < getD(a, ameise))
                        return;
                }
                int[] Weg = new Umleitung(a, ameise).Weg;
                BleibStehen();
                DreheUmWinkel(Weg[0]);
                GeheGeradeaus(Weg[1]);
            }
        }

        public override void SiehtFeind(Wanze wanze)
        {

            int alpha = Richtung;
            int beta = getA(a, wanze);
            int phi = Math.Abs(beta - alpha) % 360;
            int r = phi > 180 ? 360 - phi : phi;
            int sign = (alpha - beta >= 0 && alpha - beta <= 180) || (alpha - beta <= -180 && alpha - beta >= -360) ? 1 : -1;
            r *= sign;
            if (-30 < r && r < 30)
            {
                if (Ziel != null)
                {
                    if (getD(a, Ziel) < getD(a, wanze))
                        return;
                }
                Denke("!");
                if (getD(a, wanze) < Sichtweite / 3)
                {
                    DreheUm();
                    GeheGeradeaus(Sichtweite / 3);
                    DreheUm();
                }
                else
                {
                    BleibStehen();
                }

            }
        }

        public override void WirdAngegriffen(Ameise ameise)
        {
            
        }

        public override void WirdAngegriffen(Wanze wanze)
        {
            
        }

        #endregion

    }
}
