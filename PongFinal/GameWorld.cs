using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



class GameWorld
{
    Texture2D background, GameOver;
    Player RedPlayer, BluePlayer;
    Ball ball;
    private SpriteFont font;
    int RedScore, BlueScore;

    enum PlayingState { Playing, Menu, GameOver};
    PlayingState playingState = PlayingState.Menu;
    public GameWorld(ContentManager Content)
    {
        background = Content.Load<Texture2D>("spr_background");
        RedPlayer = new Player(Content, "red");
        BluePlayer = new Player(Content, "blue");
        font = Content.Load<SpriteFont>("font");
        ball = new Ball(Content);

    }

    public void HandleInput(ContentManager Content)
    {
        if (playingState == PlayingState.Playing)
        {
            RedPlayer.HandleInput();
            BluePlayer.HandleInput();
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && playingState == PlayingState.GameOver)
        {
            if (RedPlayer.Lives == 0)
                BlueScore++;
            else if (BluePlayer.Lives == 0)
                RedScore++;
            RedPlayer.Reset(Content);
            BluePlayer.Reset(Content);
            playingState = PlayingState.Playing;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && playingState == PlayingState.Menu)
        {
            playingState = PlayingState.Playing;
        }

    }

    public void Update(GameTime gameTime, ContentManager Content)
    {
        if (playingState == PlayingState.Playing)
        {
            ball.Update();
            if (ball.Position.X >= PongFinal.Screen.X + 50)
            {
                BluePlayer.LoseLife(Content, BluePlayer.playerColor);
            }
            if (ball.Position.X <= -50)
                RedPlayer.LoseLife(Content, RedPlayer.playerColor);

            if (RedPlayer.Lives == 0 || BluePlayer.Lives == 0)
            {
                playingState = PlayingState.GameOver;
            }
        }
        if (playingState == PlayingState.GameOver)
        {
            if (RedPlayer.Lives == 0)
            {
                if (RedScore == 0)

                GameOver = Content.Load<Texture2D>("spr_bluewinner");

            }
            else if(BluePlayer.Lives == 0)
            {
                GameOver = Content.Load<Texture2D>("spr_redwinner");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && playingState != PlayingState.Playing)
            {
                playingState = PlayingState.Playing;
            }
        }

    }
    
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (playingState == PlayingState.Playing)
        {
            spriteBatch.Begin();
            RedPlayer.Draw(gameTime, spriteBatch);
            BluePlayer.Draw(gameTime, spriteBatch);
            ball.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "" + BlueScore, new Vector2(PongFinal.Screen.X / 2 + 40, 25), Color.Black);
            spriteBatch.DrawString(font, "" + RedScore, new Vector2(PongFinal.Screen.X / 2 -68, 25), Color.Black);
            spriteBatch.DrawString(font,"-", new Vector2(PongFinal.Screen.X / 2, 25), Color.Black);
            spriteBatch.End();
        }
        if (playingState == PlayingState.Menu)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
        if (playingState == PlayingState.GameOver)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(GameOver, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }

    public Player PlayerRed
    {
        get { return RedPlayer; }
    }
    public Player PlayerBlue
    {
        get { return BluePlayer; }
    }
}
//https://imgbin.com/png/dsME3tqB/ping-pong-paddles-amp-sets-png#, the links where we got our images from, you can use it for non-commercial use.
//https://kissclipart.com/table-tennis-paddle-icon-clipart-ping-pong-paddles-unanyh/