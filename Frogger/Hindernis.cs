using System.Drawing;

namespace Frogger
{
    /// <summary>
    /// Diese Klasse beinhaltet ein Rechteck und weitere Felder, zur Darstellung eines Hindernisses.
    /// </summary>
    public class Hindernis
    {
        private Rectangle hitbox;
        private int speed;
        private SolidBrush brush;

        public Hindernis(int x, int y, int width, int height, int speed, Color color)
        {
            this.hitbox = new Rectangle(x, y, width, height);
            this.speed = speed;

            // Damit man Zeichenmittel nutzen kann, muss System.Drawing importiert werden (oben bei using...)
            this.brush = new SolidBrush(color);
        }

        public Rectangle Hitbox
        {
            get { return hitbox; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public void Move()
        {
            hitbox.X  = hitbox.X - Speed;
        }

        public SolidBrush Brush
        {
            get { return brush; }
            set { brush = value; }
        }
    }
}
