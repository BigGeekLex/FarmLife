using System;

namespace Farm.Core.Hero
{
    public interface IHero
    {
        HeroStatus GetStatus();

        event Action PlantLanded;
        event Action PlantCutted;
        event Action PlantCollected;
    }
}
