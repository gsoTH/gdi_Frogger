﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Frogger
{
    public partial class FrmFrogger : Form
    {
        static int anzahlBereiche = 6;
        // -1 ist ein Platzhalter
        int breite = -1;
        int hoehe = -1;
        int hoeheJeBereich = -1;
        Rectangle[] alleBahnen = new Rectangle[anzahlBereiche];
        List<Hindernis> alleHindernisse = new List<Hindernis>();
        Rectangle spieler;
        int spawnRate = 14;
        int spawnZaehler = 0;
        Random rndBahn = new Random();
        System.Windows.Forms.Timer tmrGame = new System.Windows.Forms.Timer();


        public FrmFrogger()
        {
            InitializeComponent();
        }

        private void FrmFrogger_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            tmrGame.Tick += new EventHandler(tmrGame_Tick);
            tmrGame.Interval = 100;
        }

        private void FrmFrogger_Paint(object sender, PaintEventArgs e)
        {
            if(tmrGame.Enabled == false)
            {
                SpielNeustarten();
            }


            SolidBrush brStart = new SolidBrush(Color.LightBlue);
            SolidBrush brZiel = new SolidBrush(Color.LightYellow);
            SolidBrush brBahnHell = new SolidBrush(Color.LightGray);
            SolidBrush brBahnDunkel = new SolidBrush(Color.Gray);
            SolidBrush brSpieler = new SolidBrush(Color.Green);
            Pen pnRand = new Pen(Color.Black, 1);

            e.Graphics.FillRectangles(brBahnHell, alleBahnen);

            // TODO: Bahnen sollen sich in der Farbe abwechseln (brBahnHell, brBahnDunkel)

            e.Graphics.FillRectangle(brZiel, alleBahnen[0]);
            e.Graphics.FillRectangle(brStart, alleBahnen[alleBahnen.Length-1]);
            
            e.Graphics.DrawRectangles(pnRand, alleBahnen);

            foreach(Hindernis aktuellesHindernis in alleHindernisse)
            {
                e.Graphics.FillRectangle(
                    aktuellesHindernis.Brush,
                    aktuellesHindernis.Hitbox);
            }

            e.Graphics.FillEllipse(brSpieler, spieler);


        }

        private void SpielNeustarten()
        {
            breite = this.ClientSize.Width;
            hoehe = this.ClientSize.Height;

            hoeheJeBereich = hoehe / anzahlBereiche;

            //Puffer für Rundungsfehler
            hoeheJeBereich = hoeheJeBereich + 2;

            spieler = new Rectangle((breite / 2) - 15, hoehe - 35, 30, 30);

            for (int i = 0; i < alleBahnen.Length; i++)
            {
                alleBahnen[i] = new Rectangle(0, i * hoeheJeBereich, breite, hoeheJeBereich);
            }

            tmrGame.Start();
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            HindernisseErstellen();
            HindernisseBewegen();
            HindernisseEntfernen();

            // TODO Kontrollieren, ob Spieler überfahren wurde.

            this.Refresh();
        }

        private void HindernisseEntfernen()
        {
            for (int i = alleHindernisse.Count - 1; i >= 0; i--)
            {
                if ((alleHindernisse[i].Hitbox.X + alleHindernisse[i].Hitbox.Width) < 0)
                {
                    alleHindernisse.RemoveAt(i);

                }
            }
        }

        private void HindernisseBewegen()
        {
            foreach (Hindernis aktuellesHindernis in alleHindernisse)
            {
                aktuellesHindernis.Move();
            }
        }

        private void HindernisseErstellen()
        {
            spawnZaehler++;
            if (spawnZaehler == spawnRate)
            {
                spawnZaehler = 0;

                int zufall = rndBahn.Next(1, anzahlBereiche - 1);
                int yWertDerBahn = alleBahnen[zufall].Top;

                alleHindernisse.Add(new Hindernis(breite, yWertDerBahn, 60, hoeheJeBereich, 10, Color.Red));
            }
        }

        private void FrmFrogger_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO Weitere Bewegungen (links, rechts) einbauen

            if (e.KeyCode == Keys.Up)
            {
                // TODO Wenn Spieler im Ziel ist, soll er wieder auf Start zurückgesetzt werden und das Spiel soll schwerer werden
                spieler.Y = spieler.Y - hoeheJeBereich;
            }

            if(e.KeyCode == Keys.Down)
            {
                // TODO Spieler darf nicht nach unten aus dem Spielfeld laufen
                spieler.Y = spieler.Y + hoeheJeBereich;
            }

            this.Refresh();
        }
    }
}
