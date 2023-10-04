using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System; 

class PongFinal : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    static GameWorld gameWorld;
    static Vector2 screen = new Vector2(800, 800);


    [STAThread]
    static void Main()
    {
        using (var game = new PongFinal())
            game.Run();
    }
    public PongFinal()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        graphics.PreferredBackBufferHeight = (int)screen.Y;
        graphics.PreferredBackBufferWidth = (int)screen.X;
        graphics.ApplyChanges();
    }

    protected override void LoadContent()
    {

        spriteBatch = new SpriteBatch(GraphicsDevice);
        gameWorld = new GameWorld(Content);
    }

    protected void Update1(GameTime gameTime, ContentManager Content)
    {
        gameWorld.HandleInput(Content);
        gameWorld.Update(gameTime, Content);
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
    }

    protected override void Update(GameTime gameTime)
    {
        Update1(gameTime, Content);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        gameWorld.Draw(gameTime, spriteBatch);
    }

    public static GameWorld GameWorld
    {
        get { return gameWorld; }
    }
    public static Vector2 Screen
    {
        get { return screen; }
    } 
    //Deze class heeft zo min mogelijk met de game te maken zodat gamecode gescheiden is van de code die de game laat runnen, op deze manier blijft het overzichtelijk. 
    //Deze manier van de game opbouwen wordt uitgelegd in hoofstuk 7 van het boek. Ik heb code van "painter4" (zie hoofdstuk 7) gebruikt als template om Pong in te maken. 
}
