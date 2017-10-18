using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntMe.Deutsch;

namespace AntMe.Player.AntMe.Ameisen
{
    
public class Umleitung {

	public Insekt Hindernis;
	public AntMeKlasse ego;

	private int enemySpeed;
	private int enemyDistance;
	private int enemyAngle;
	private int ownSpeed;

	private int escTurns;

	// output Variablen
	private double escAngle;
	private double escDistance;

	public int[] Weg = new int[2]; 

	public Umleitung(AntMeKlasse ego, Insekt Hindernis)
	{
		this.ego = ego;
		this.Hindernis = Hindernis;
		calcParameters();
        //ego.Denke(((int)escAngle).ToString());
		Weg[0] = (int)escAngle;
		Weg[1] = (int)escDistance;
	}

	private void calcParameters()
	{
		enemyDistance = Koordinate.BestimmeEntfernung(ego, Hindernis);
		enemySpeed = Hindernis.MaximaleGeschwindigkeit;
        enemyAngle = direction(ego.Richtung, Koordinate.BestimmeRichtung(ego, Hindernis));
		ownSpeed = ego.MaximaleGeschwindigkeit;

		if(enemyDistance > enemySpeed)
        {
			escTurns = (int) Math.Ceiling(10*enemySpeed / (double) ownSpeed);
			escAngle = ((enemyDistance / enemySpeed - escTurns) *6) * enemyAngle;
			escDistance = escTurns * ownSpeed;
		}
	}

    private int direction(int alpha, int beta)
    {
        int phi = Math.Abs(beta - alpha) % 360;       // This is either the distance or 360 - distance
        int difference = phi > 180 ? 360 - phi : phi;
        int sign = (alpha - beta >= 0 && alpha - beta <= 180) || (alpha - beta <= -180 && alpha - beta >= -360) ? 1 : -1;
        return sign;
    }

    private int difference(int alpha, int beta)
    {
        int phi = Math.Abs(beta - alpha) % 360;       // This is either the distance or 360 - distance
        int difference = phi > 180 ? 360 - phi : phi;
        int sign = (alpha - beta >= 0 && alpha - beta <= 180) || (alpha - beta <= -180 && alpha - beta >= -360) ? 1 : -1;
        difference *= sign;
        return difference;
    }
}

}
