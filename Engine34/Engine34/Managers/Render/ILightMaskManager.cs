using Engine34.Managers.Render;

namespace Engine.Managers.Render
{
    public interface ILightMaskManager
    {
        void addMask(ILightMask mask);


        void RemoveLightSource(ILightMask x);

        void ClearLightMasks();

    }
}