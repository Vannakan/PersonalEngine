using Engine.Events.KeyboardEvent;
using Engine.Interfaces.Cam;
using Engine.Interfaces.Resource;
using Engine.Interfaces.Screen;
using Engine.Managers.Cam;
using Engine.Managers.ServiceLocator;
using Engine.States.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameCode.Screens.GameOptions
{
    /// <summary>
    /// CLASS: The splash screen that is loaded upon running the program whilst the main menu loads
    /// </summary>
    class SplashScreen : BaseScreen
    {
        /// <summary>
        /// GET: The Camera Manager from the service locator;
        /// </summary>
        ICameraManager cam = Locator.Instance.getService<ICameraManager>();

        /// <summary>
        /// DECLARE: The transition variables for this screen
        /// </summary>
        float alpha = 0;
        bool hasEnded = false;
        bool transIn = true;
        bool transOut = false;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public SplashScreen() { }

        /// <summary>
        /// METHOD: Initialises the logic for the splash screen
        /// </summary>
        public override void Initialize()
        {

            ///CALL: BaseScreen Initialize method
            base.Initialize();
        }

        /// <summary>
        /// METHOD: Draws the screen content
        /// </summary>
        /// <param name="sb">Monogame SpriteBatch</param>
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Locator.Instance.getService<IResourceLoader>().GetTex("MedicationLogo"), cam.getWorldPosition(new Vector2(275, 50)), Color.White * alpha);
            base.Draw(sb);
        }

        /// <summary>
        /// METHOD: Update which is cycled through each frame and contains the logic for the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            ///IF: Transitioning in increase the alpha to become more opaque
            if (transIn)
                alpha += 0.010f;
   ///ELSE IF: transitioning out then decrease the alpha
   else if (transOut)
                alpha -= 0.010f;

            ///IF Fully opaque
            if (alpha >= 1)
            {
                ///SET: Transition phase
                transOut = true;
                transIn = false;
            }
            ///IF: Fully transparent
            if (alpha <= 0 && transOut)
            {
                ///CALL: ReplaceScreen with MainMenu
                Locator.Instance.getService<IScreenManager>().ReplaceScreen<MainMenu>("MainMenu");

            }
            ///CALL: BaseScreen Update
            base.Update(gameTime);
        }
    }
}