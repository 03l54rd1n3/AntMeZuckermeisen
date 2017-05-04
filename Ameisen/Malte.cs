using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntMe.Deutsch;

namespace AntMe.Player.AntMe.Ameisen
{
    public class Malte : Brain
    {

        public Malte(AntMeKlasse _a)
        {
            a = _a;
        }

        public override void Init()
        {
            if (bau == null)
            {
                Storage.ants.Add(this);
                Denke(Storage.ants.Count.ToString() + " - " + Kaste);
                GeheZuBau();
                bau = (Bau)Ziel;
                BleibStehen();
                if (sugar == null)
                {
                    DreheUmWinkel(new Random().Next(360));
                    GeheGeradeaus();
                }
            }
        }

        private bool evading = false;

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
            evading = false;
            if (AktuelleLast == 0)
            {
                if (sugar != null)
                {
                    if (sugar.Menge > 0)
                    {
                        Sprint(sugar);
                    }
                    else
                    {
                        sugar = null;
                    }
                }
                if (sugar == null)
                {
                    foreach (Brain ant in Storage.ants)
                    {
                        if (ant.Kaste == Kaste && ant.sugar != null && ant.sugar.Menge > 0)
                        {
                            sugar = ant.sugar;
                        }
                    }
                    
                }
            }
            else if (AktuelleLast == MaximaleLast && !evading)
            {
                Sprint(bau);
            }

        }

        public override void WirdMüde()
        {

        }
        
        public override void IstGestorben(Todesart todesart)
        {
            Denke(((Todesart)todesart).ToString());
            Storage.ants.Remove(this);
        }

        public override void Tick()
        {
            //Denke(Kaste);
            if (Ziel != apple && GetragenesObst != apple)
            {
                apple = null;
                Denke("OhOh");
            }
           
            if(GetragenesObst != null)
            {

            }
        }

        #endregion

        #region Nahrung
        
        public override void Sieht(Obst obst)
        {
            if (AktuelleLast == 0 && obst.Menge / MaximaleLast > 0)
            {
                try
                {
                    if (sugar != null && getD(a, obst) + getD(obst, bau) > getD(a, sugar) + getD(sugar, bau))
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Denke(ex.Message);
                }
                

                apple = obst;
                sugar = null;
                Sprint(apple);
            }
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

            if (((sugar == null && AktuelleLast == 0) || (AktuelleLast < MaximaleLast)) && apple == null)
            {
                sugar = zucker;
                Sprint(sugar);
            }

        }

        public override void ZielErreicht(Obst obst)
        {
            if(AktuelleLast == 0)
                Nimm(obst);
            if(BrauchtNochTräger(obst))
            {
                int n = 0;
                foreach(KeyValuePair<Obst,int> o in Storage.obst)
                {
                    if (o.Key == obst)
                    {
                        n++;
                        break;
                    }
                    n++;
                }
                if (n == 0)
                {
                    Storage.obst.Add(obst, obst.Menge/MaximaleLast);
                    n = Storage.obst.Count;
                }
                SprüheMarkierung(2000 + n, 1000);
            }
            Sprint(bau);
        }

        public override void ZielErreicht(Zucker zucker)
        {
            if (AktuelleLast < MaximaleLast)
            {
                try
                {
                    Nimm(zucker);
                }
                catch (Exception ex)
                {

                }

                Sprint(bau);
            }
        }

        #endregion

        #region Kommunikation

        public override void RiechtFreund(Markierung markierung)
        {
            if (markierung.Information > 1000 && markierung.Information < 2000)
            {
                Zucker z = Storage.zucker[markierung.Information - 1000 - 1];
                try
                {
                    if (z != null)
                    {
                        if (sugar != null && bau != null)
                        {
                            if ((getD(bau, z) > getD(bau, sugar)) && getD(a, z)
                                + getD(z, bau) > getD(a, sugar) + getD(sugar, bau))
                            {
                                //Denke("Inefficient");
                                return;
                            }
                        }

                        sugar = z;

                        if (Ziel == null && AktuelleLast < MaximaleLast && !evading)
                        {
                            if (AktuelleLast < MaximaleLast && Ziel != sugar && Ziel != bau && Ziel != apple)
                            {
                                Sprint(sugar);
                            }
                        }
                        else
                        {
                            if (AktuelleLast == MaximaleLast && !evading)
                                Sprint(bau);
                        }
                    }
                }
                catch
                {
                    Denke("OMG Exception");
                    Storage.zucker.RemoveAt(markierung.Information - 1000 - 1);
                }
            }
            else if (markierung.Information > 2000)
            {
                KeyValuePair<Obst,int> dict = Storage.obst.ElementAt<KeyValuePair<Obst, int>>(markierung.Information - 2000 - 1);
                Obst o = dict.Key;
                Denke(dict.Value.ToString());
                if (dict.Value > 0 && AktuelleLast == 0 && apple != o)
                {
                    if (apple != null)
                    {
                        if (getD(a, apple) < getD(a, o))
                            return;
                    }
                    Storage.obst[o]--;
                    apple = o;
                    Sprint(apple);
                }
            }
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
                evading = true;
                BleibStehen();
                DreheUmWinkel(Weg[0]);
                GeheGeradeaus(Weg[1]);
                if(Weg[0] == 0 && Weg[1] == 0)
                {
                    if (bau != null && getD(a, bau) < 700)
                        SprüheMarkierung(AnzahlFremderAmeisenInSichtweite + AnzahlAmeisenDesTeamsInSichtweite, 700);
                }
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
            if(bau != null && getD(a, bau) < 700)
                SprüheMarkierung(AnzahlFremderAmeisenInSichtweite + AnzahlAmeisenDesTeamsInSichtweite, 700);
        }

        public override void WirdAngegriffen(Wanze wanze)
        {

        }

        #endregion

    }
}
