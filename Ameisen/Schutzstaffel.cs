using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntMe.Deutsch;

namespace AntMe.Player.AntMe.Ameisen
{
    public class Schutzstaffel : Brain
    {
        
        public Schutzstaffel(AntMeKlasse _a)
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
            }
        }

        #region Fortbewegung

        public void Sprint(Spielobjekt ziel)
        {
            if (ziel != null)
            {
                GeheZuZiel(ziel);
                int d = getD(a, ziel);
                int _a = getA(a, ziel);
                DreheInRichtung(_a);
                GeheGeradeaus(d);

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
            if(getD(a, bau) > 5 )
                Sprint(bau);
            else
                DreheUmWinkel(359);
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
        }

        #endregion

        #region Nahrung
        
        public override void Sieht(Obst obst)
        {
            
        }

        public override void Sieht(Zucker zucker)
        {

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
            if(markierung.Information < 1000 && markierung.Information > 0)
            {
                if(target == null)
                {
                    target = markierung;
                    Sprint(markierung);
                }
                else
                {
                        try
                        {
                            if (markierung.Information > ((Markierung)target).Information)
                            {
                                target = markierung;
                                Sprint(markierung);
                            }
                        }
                        catch
                        {
                            if (getD(a, markierung) < getD(a, target))
                            {
                                target = markierung;
                                Sprint(markierung);
                            }
                        }
                    
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
            target = ameise;
            GreifeAn(ameise);
        }

        public override void SiehtFeind(Wanze wanze)
        {
            if(wanze != null && bau != null && wanze.AktuelleEnergie > 0)
            {
                if (getD(wanze, bau) < 5)
                    GreifeAn(wanze);
            }
        }

        public override void WirdAngegriffen(Ameise ameise)
        {
            if(bau != null && getD(a, bau) < 700)
                SprüheMarkierung(AnzahlAmeisenDerSelbenKasteInSichtweite, 700);
        }

        public override void WirdAngegriffen(Wanze wanze)
        {
            try
            {
                GreifeAn(wanze);
            }
            catch
            {

            }

        }

        #endregion

    }
}
