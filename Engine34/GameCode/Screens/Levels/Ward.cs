using Engine.Interfaces.Cam;
using Engine.Interfaces.Collision;
using Engine.Interfaces.Entities;
using Engine.Interfaces.Resource;
using Engine.Managers.ServiceLocator;
using Engine.States.Engine;
using Engine34.Managers.Cam;
using GameCode.Entities.Human;
using GameCode.Entities.Terrain;
using GameCode.Entities.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameCode.Screens.Levels
{
    class Ward : BaseScreen
    {
        Texture2D mesh;
        public override void Initialize()
        {
            SoundTrack = "SoundTrack1";
         
            Locator.Instance.getService<IEntityManager>().createEntityDrawable<HospitalBed>(new Vector2(128, 128));
           mesh =  Locator.Instance.getService<IResourceLoader>().GetTex("map");
            ///ADD: Entities used in the ward level.
            Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BenTipton>(new Vector2(500, 400));
            Locator.Instance.getService<ICameraManager>().getCam().setEntity(Locator.Instance.getService<IEntityManager>().getCamEntity("C"), CameraState.LOCKED);
            Locator.Instance.getService<ICameraManager>().getCam().Zoom = 0.9f;
            Locator.Instance.getService<IEntityManager>().createEntityDrawable<FemaleNurse>(new Vector2(600, 400));


            base.Initialize();
        }

        public override void Unload()
        {
            Locator.Instance.getService<IDetectionManager>().ClearCollisionList();
            base.Unload();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(mesh, Vector2.Zero, Color.White);
            base.Draw(spriteBatch);
        }


        ///MIKES METHOD
        //private void createWard()
        //{
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(-470, 180));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(-520, 220));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(-430, 270));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlackHairPatient>(new Vector2(-465, 240));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(-520, 240));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<GingerHairPatient>(new Vector2(-552, 215));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(-510, 275));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(-530, 280));

        //    //Tables for Corridor inbetween bottom left ward and waiting room
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(-520, 80));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(-520, 150));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(-400, 80));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(-400, 150));

        //    //Waiting Room texture positions
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(-480, 0));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(-520, -20));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(-520, -100));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(-400, -20));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(-400, -100));

        //    //Top Left Ward texture positions
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(-495, -250));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(-520, -220));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(-520, -300));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(-450, -300));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(-380, -300));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlackHairPatient>(new Vector2(-555, -250));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlackHairPatient>(new Vector2(-485, -330));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<GingerHairPatient>(new Vector2(-555, -330));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<GingerHairPatient>(new Vector2(-415, -330));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(-500, -310));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(-425, -310));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<WaterBottle>(new Vector2(-320, -290));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<WaterBottle>(new Vector2(-320, -250));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<WaterBottle>(new Vector2(-350, -290));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<WaterBottle>(new Vector2(-350, -250));

        //    //Bottom Left Isolation Room
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(-330, 240));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlondeHairPatient>(new Vector2(-365, 210));

        //    //Bottom Middle Left Beer Storage
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(-290, 180));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(-290, 220));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(-290, 240));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(-260, 180));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(-260, 220));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(-260, 240));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(-240, 180));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(-240, 240));

        //    //Meeting Room
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(0, 250));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(70, 280));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(50, 280));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(30, 280));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(10, 280));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(-10, 280));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(-30, 280));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(-50, 280));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(-70, 280));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(-10, 230));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(-30, 230));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(10, 230));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Stool>(new Vector2(30, 230));
        //    //Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(0, 180));
        //    //Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(0, 180));
        //    //Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(0, 180));
        //    //Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(0, 180));
        //    //Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(0, 180));
        //    //Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Beer>(new Vector2(0, 180));

        //    //Bottom Middle Right Isolation Room
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(185, 230));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlondeHairPatient>(new Vector2(150, 200));

        //    //Bottom Right Isolation Room
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(270, 230));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlondeHairPatient>(new Vector2(235, 200));

        //    //Top Left Chloroform Storage
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Chloroform>(new Vector2(-290, -70));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Chloroform>(new Vector2(-290, -40));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Chloroform>(new Vector2(-260, -70));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Chloroform>(new Vector2(-260, -40));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Chloroform>(new Vector2(-230, -40));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Chloroform>(new Vector2(-230, -70));

        //    //Middle Ward
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(0, -50));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(90, -80));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(70, -120));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(30, -120));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(90, -20));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(-20, -20));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlackHairPatient>(new Vector2(55, -105));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlackHairPatient>(new Vector2(-5, -145));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlondeHairPatient>(new Vector2(35, -145));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlondeHairPatient>(new Vector2(-55, -45));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<GingerHairPatient>(new Vector2(55, -50));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(90, 30));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(-20, 30));

        //    //Nurse Staff Room
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(120, -120));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Table>(new Vector2(190, -100));

        //    //Bottom Right Ward
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(390, -100));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(390, -200));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(390, 190));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(390, 125));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(400, 40));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(400, -120));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(400, 190));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(500, 190));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(500, -190));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(500, 40));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(500, -120));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlackHairPatient>(new Vector2(365, 10));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<GingerHairPatient>(new Vector2(365, -150));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<GingerHairPatient>(new Vector2(365, 175));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<GingerHairPatient>(new Vector2(465, 175));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<GingerHairPatient>(new Vector2(465, -220));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlondeHairPatient>(new Vector2(465, 10));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlondeHairPatient>(new Vector2(465, -150));

        //    //Top Right Ward
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(440, -300));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(370, -300));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Bed>(new Vector2(410, -300));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlondeHairPatient>(new Vector2(330, -330));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BlondeHairPatient>(new Vector2(380, -330));

        //    //Storage Needle/Saw
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Needle>(new Vector2(310, -250));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Needle>(new Vector2(330, -250));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Needle>(new Vector2(290, -250));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Needle>(new Vector2(280, -250));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Saw>(new Vector2(310, -220));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Saw>(new Vector2(330, -220));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Saw>(new Vector2(290, -220));

        //    //Middle Corridor 
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(100, 70));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(-170, 70));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Nurse>(new Vector2(240, 120));
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<BenTipton>(Vector2.Zero);
        //    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<CharlesHastings>(Vector2.Zero);
        //}

    }
}
