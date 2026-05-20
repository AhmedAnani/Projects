using Animal_Shelter_V2.src.Models.implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animal_Shelter_V2.src.Factory.Interfaces
{
    internal interface IAnimalFactory
    {
        public static Dog BuildDog(int id);

        public static Cat BuildCat(int id);

        public static Bird BuildBird(int id);

        public static SmallAnimal BuildSmallAnimal(int id);
    }
}
