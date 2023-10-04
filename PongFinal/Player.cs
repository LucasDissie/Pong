using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;



class Player
{
    Vector2 Positie, LifePos;
    Texture2D PaddleSprite, Lifebar;
    int Speed, lives = 3;
    string color;
    bool invincibilty = false;

    public Player(ContentManager Content, string constructcolor)
    {
        if (constructcolor == "red")
        {
            this.PaddleSprite = Content.Load<Texture2D>("spr_rodeSpeler");
            Lifebar = Content.Load<Texture2D>("redLifeBar3");
            Positie = new Vector2(0, PongFinal.Screen.Y / 2 - PaddleSprite.Height / 2);
            LifePos = new Vector2(PongFinal.Screen.X / 4 - Lifebar.Width / 2, 20);
            Speed = 5;
            color = "red";
        }
        else if (constructcolor == "blue")
        {
            this.PaddleSprite = Content.Load<Texture2D>("spr_blauweSpeler");
            Lifebar = Content.Load<Texture2D>("blueLifeBar3");
            LifePos = new Vector2(PongFinal.Screen.X / 4 * 3 - Lifebar.Width / 2, 20);
            Positie = new Vector2(PongFinal.Screen.X - 16, PongFinal.Screen.Y / 2 - PaddleSprite.Height / 2);
            Speed = 5;
            color = "blue";
        }
    }
    public void Reset(ContentManager Content)
    {
        if (color == "red")
        {
            Positie = new Vector2(0, 400);
            Lifebar = Content.Load<Texture2D>("redLifeBar3");
            lives = 3;
        }
        else if (color == "blue")
        {
            Positie = new Vector2(PongFinal.Screen.X - 16, 400);
            Lifebar = Content.Load<Texture2D>("blueLifeBar3");
            lives = 3;
        }
    }
    public void HandleInput()
    {
        Keys Up = Keys.Up;
        Keys Down = Keys.Down;
        if (color == "blue")
        {
            Up = Keys.Up;
            Down = Keys.Down;
        }
        else if (color == "red")
        {
            Up = Keys.W;
            Down = Keys.S;
        }
        if (Keyboard.GetState().IsKeyDown(Down))
        {
            if (Positie.Y + Speed >= PongFinal.Screen.Y - 94)
            {
            }
            else
            {
                Positie.Y += Speed;
            }
        }
        if (Keyboard.GetState().IsKeyDown(Up))
        {
            if (Positie.Y - Speed > -2)
            {
                Positie.Y -= Speed;
            }
        }
    }

    public void LoseLife(ContentManager Content, string color)
    {
        if (!invincibilty)
        {
            lives -= 1;
            invincibilty = true;
            if (color == "red")
            {
                if (lives == 2)
                    Lifebar = Content.Load<Texture2D>("redLifeBar2");
                else if (lives == 1)
                    Lifebar = Content.Load<Texture2D>("redLifeBar1");          
            }
            else
            {
                if (lives == 2)
                    Lifebar = Content.Load<Texture2D>("blueLifeBar2");
                else
                    Lifebar = Content.Load<Texture2D>("blueLifeBar1");
            }
        } 
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(PaddleSprite, Positie, Color.White);
        spriteBatch.Draw(Lifebar, LifePos, Color.White);  
    }
    public Vector2 Position
    {
        get { return Positie;}
    }
    public int OriginPosY
    {
        get { return (int)Positie.Y + (PaddleSprite.Height / 2); }
    }

    public int Lives
    {
        get { return lives; }
    }

    public string playerColor
    {
        get { return color; }
    }

    public bool Invincibility
    {
        get { return invincibilty; }
        set { invincibilty = value; }
    }
}


