using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Morgly.Application
{
    public abstract class Animal
    {
        protected int NumberOfLegs;
        protected string _sound;

        protected Animal(int numberOfLegs, string sound = "No sound")
        {
            NumberOfLegs = numberOfLegs;
            _sound = sound;
        }
        
        // Add metod that returns the number of legs
        public int GetNumberOfLegs()
        {
            return NumberOfLegs;
        }

        // Create class that returns the sound of the animal
        public virtual string GetSound()
        {
            return _sound;
        }


        public virtual void SetSound(string sound)
        {
            _sound = sound;
        }
    }

    // Create a class that inherits from Animal named Dog
    public class Dog : Animal
    {
        public Dog() : base(4, "Woof")
        {
        }

    }
}
