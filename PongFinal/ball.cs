using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

class Ball
{
    Vector2 Positie, Angle;
    Texture2D BallSprite;
    int mirror;
    bool Launch = true;
    Random random = new Random();
    float prevYpos, prevXpos, speed = 3.5f;
    string LastBounce;

    public Ball(ContentManager Content)
    {
        this.BallSprite = Content.Load<Texture2D>("spr_ball");
        Positie = new Vector2(400, 400);
    }

    public void Update()
    {
        prevYpos = Positie.Y;
        prevXpos = Positie.X;
        LaunchBall();
        if (Positie.X > (PongFinal.Screen.X / 2))
            BounceBall(PongFinal.GameWorld.PlayerBlue.OriginPosY, "blue");
        else
            BounceBall(PongFinal.GameWorld.PlayerRed.OriginPosY, "red");
            Positie += Normalize(Angle) * speed;
    }

    public void Reset()
    {
        Positie = new Vector2(400, 400);
        speed = 4.0f;
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(BallSprite, Positie, Color.White);
    }
    public void LaunchBall()
    {
        if (Positie.X <= -200 || Positie.X >= PongFinal.Screen.X + 200)
        {
            Launch = true;
        }
        if (Launch)
        {
            Reset();
            PongFinal.GameWorld.PlayerRed.Invincibility = false;
            PongFinal.GameWorld.PlayerBlue.Invincibility = false;

            Angle.Y = (float)(random.NextDouble() * 2) - 1;
            mirror = random.Next(0, 2);
            if (mirror == 1)
            {
                Angle.X = 1;
                LastBounce = "red";
            }
            else
            {
                Angle.X = -1;
                LastBounce = "blue";
            }
            Launch = false;
        }
    }
    public void BounceBall(float PlayerPosY, string color)
    {

        float a = (Angle.Y) / (Angle.X), y = 0f;
        float b = prevYpos - a * prevXpos;
        if (color == "red")
        {
            if (16 >= Positie.X && LastBounce != "red")
            {
                y = a * 16 + b;
                LastBounce = "red";
                if (Math.Abs(PlayerPosY - y) <= 52)
                {
                    Bounce(PongFinal.GameWorld.PlayerRed.OriginPosY);
                }
            }
        }
        else
        {
            if (PongFinal.Screen.X - 32 <= Positie.X && LastBounce != "blue")
            {
                y = a * (PongFinal.Screen.X - 32) + b;
                LastBounce = "blue";
                if (Math.Abs(PlayerPosY - y) <= 52)
                {
                    Bounce(PongFinal.GameWorld.PlayerBlue.OriginPosY);      
                }
            }
        }
        if (Positie.Y <= 0 || Positie.Y >= PongFinal.Screen.Y - 16)
        {
            Angle.Y *= -1;
        }
    }

    public void Bounce(int OriginPosY)
    {
        speed *= 1.05f;
        Angle.Y += (OriginPosY - Positie.Y) / -104;
        if (Angle.Y >= 1)
            Angle.Y = 1;
        if (Angle.Y <= -1)
            Angle.Y = -1;
        Angle.X *= -1;
        
     
    }
    public Vector2 Normalize (Vector2 vector)
    {
        float devider = (float)(1 / Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y));
        return vector * devider;
    }

    public Vector2 Position
    {
        get { return Positie; }
    }
}


