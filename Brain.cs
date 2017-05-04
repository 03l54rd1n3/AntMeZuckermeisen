using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntMe.Deutsch;

namespace AntMe.Player.AntMe
{
    public abstract class Brain
    {

        public AntMeKlasse a;
        public Bau bau;
        public Zucker sugar;
        public Obst apple;
        public Spielobjekt target;

        public abstract void Init();

        #region Fortbewegung

        public abstract void Wartet();

        public abstract void WirdMüde();
        
        public abstract void IstGestorben(Todesart todesart);

        public abstract void Tick();

        #endregion

        #region Nahrung
        
        public abstract void Sieht(Obst obst);

        public abstract void Sieht(Zucker zucker);

        public abstract void ZielErreicht(Obst obst);

        public abstract void ZielErreicht(Zucker zucker);

        #endregion

        #region Kommunikation

        public abstract void RiechtFreund(Markierung markierung);

        public abstract void SiehtFreund(Ameise ameise);

        public abstract void SiehtVerbündeten(Ameise ameise);

        #endregion

        #region Kampf

        public abstract void SiehtFeind(Ameise ameise);

        public abstract void SiehtFeind(Wanze wanze);

        public abstract void WirdAngegriffen(Ameise ameise);

        public abstract void WirdAngegriffen(Wanze wanze);

        #endregion

        #region Wrapper

        public void GeheGeradeaus()
        {
            a.GeheGeradeaus();
        }

        public void GeheGeradeaus(int d)
        {
            a.GeheGeradeaus(d);
        }

        public void GeheZuZiel(Spielobjekt z)
        {
            a.GeheZuZiel(z);
        }

        public void GeheWegVon(Spielobjekt z)
        {
            a.GeheWegVon(z);
        }

        public void GeheZuBau()
        {
            a.GeheZuBau();
        }

        public void BleibStehen()
        {
            a.BleibStehen();
        }

        public void DreheInRichtung(int w)
        {
            a.DreheInRichtung(w);
        }

        public void DreheUm()
        {
            a.DreheUm();
        }

        public void DreheUmWinkel(int w)
        {
            a.DreheUmWinkel(w);
        }

        public void DreheZuZiel(Spielobjekt z)
        {
            a.DreheZuZiel(z);
        }

        public void Nimm(Nahrung n)
        {
            a.Nimm(n);
        }

        public void LasseNahrungFallen()
        {
            a.LasseNahrungFallen();
        }

        public void SprüheMarkierung(int info)
        {
            a.SprüheMarkierung(info);
        }

        public void SprüheMarkierung(int info, int größe)
        {
            a.SprüheMarkierung(info, größe);
        }

        public void GreifeAn(Insekt z)
        {
            a.GreifeAn(z);
        }

        public void Denke(string txt)
        {
            a.Denke(txt);
        }

        public bool BrauchtNochTräger(Obst o)
        {
            return a.BrauchtNochTräger(o);
        }

        public int getD(Simulation.CoreAnt a, Simulation.CoreAnt b)
        {
            return Koordinate.BestimmeEntfernung(a, b);
        }

        public int getD(Spielobjekt a, Simulation.CoreAnt b)
        {
            return Koordinate.BestimmeEntfernung(a, b);
        }

        public int getD(Simulation.CoreAnt a, Spielobjekt b)
        {
            return Koordinate.BestimmeEntfernung(a, b);
        }

        public int getD(Spielobjekt a, Spielobjekt b)
        {
            return Koordinate.BestimmeEntfernung(a, b);
        }

        public int getA(Simulation.CoreAnt a, Simulation.CoreAnt b)
        {
            return Koordinate.BestimmeRichtung(a, b);
        }

        public int getA(Spielobjekt a, Simulation.CoreAnt b)
        {
            return Koordinate.BestimmeRichtung(a, b);
        }

        public int getA(Simulation.CoreAnt a, Spielobjekt b)
        {
            return Koordinate.BestimmeRichtung(a, b);
        }

        public int getA(Spielobjekt a, Spielobjekt b)
        {
            return Koordinate.BestimmeRichtung(a, b);
        }

        public int MaximaleEnergie { get { return a.MaximaleEnergie; } }
        public int MaximaleGeschwindigkeit { get { return a.MaximaleGeschwindigkeit; } }
        public int MaximaleLast { get { return a.MaximaleLast; } }
        public int Reichweite { get { return a.Reichweite; } }
        public int Angriff { get { return a.Angriff; } }
        public int Sichtweite { get { return a.Sichtweite; } }
        public int Drehgeschwindigkeit { get { return a.Drehgeschwindigkeit; } }
        public int AktuelleEnergie { get { return a.AktuelleEnergie; } }
        public int AktuelleGeschwindigkeit { get { return a.AktuelleGeschwindigkeit; } }
        public int AktuelleLast { get { return a.AktuelleLast; } }
        public int WanzenInSichtweite { get { return a.WanzenInSichtweite; } }
        public int AnzahlFremderAmeisenInSichtweite { get { return a.AnzahlFremderAmeisenInSichtweite; } }
        public int AnzahlAmeisenInSichtweite { get { return a.AnzahlAmeisenInSichtweite; } }
        public int AnzahlAmeisenDerSelbenKasteInSichtweite { get { return a.AnzahlAmeisenDerSelbenKasteInSichtweite; } }
        public int AnzahlAmeisenDesTeamsInSichtweite { get { return a.AnzahlAmeisenDesTeamsInSichtweite; } }
        public int EntfernungZuBau { get { return a.EntfernungZuBau; } }
        public Obst GetragenesObst { get { return a.GetragenesObst; } }
        public string Kaste { get { return a.Kaste; } }
        public Spielobjekt Ziel { get { return a.Ziel; } }
        public bool IstMüde { get { return a.IstMüde; } }
        public int RestStrecke { get { return a.RestStrecke; } }
        public int RestWinkel { get { return a.RestWinkel; } }
        public int Richtung { get { return a.Richtung; } }
        public bool Angekommen { get { return a.Angekommen; } }
        public int ZurückgelegteStrecke { get { return a.ZurückgelegteStrecke; } }

        #endregion

    }
}
