
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Utilities.Messages;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private List<IAquarium> aquariums;
        private DecorationRepository decorations;

        public Controller()
        {
            aquariums = new List<IAquarium>();
            decorations = new DecorationRepository();
        }

        public string AddAquarium(string aquariumType, string aquariumName)
        {
           IAquarium aquarium = default;

            switch (aquariumType)
            {
                case nameof(FreshwaterAquarium):
                    aquarium = new FreshwaterAquarium(aquariumName);
                    break;

                case nameof(SaltwaterAquarium):
                    aquarium = new SaltwaterAquarium(aquariumName);
                    break;

                default: throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            aquariums.Add(aquarium);
            return String.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration = default;

            switch (decorationType)
            {
                case nameof(Ornament):
                    decoration = new Ornament();
                    break;

                case nameof(Plant):
                    decoration = new Plant();
                    break;
                
                default: throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            decorations.Add(decoration);
            return String.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration desireDecoration = decorations.FindByType(decorationType);

            if (desireDecoration == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InexistentDecoration, decorationType));
            }

            IAquarium desireAquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            
            desireAquarium.AddDecoration(desireDecoration);
            decorations.Remove(desireDecoration);
            return String.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish newFish = default;
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);

            switch (fishType)
            {
                case nameof(FreshwaterFish):
                    newFish = new FreshwaterFish(fishName, fishSpecies, price);

                    if (aquarium.GetType().Name != nameof(FreshwaterAquarium))
                    {
                        return OutputMessages.UnsuitableWater;
                    }
                    break;

                case nameof(SaltwaterFish):
                    newFish = new SaltwaterFish(fishName, fishSpecies, price);

                    if (aquarium.GetType().Name != nameof(SaltwaterAquarium))
                    {
                        return OutputMessages.UnsuitableWater;
                    }
                    break;

                default: throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            aquarium.AddFish(newFish);
            return String.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            aquarium.Feed();

            return String.Format(OutputMessages.FishFed, aquarium.Fish.Count);
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            var fishValue = aquarium.Fish.Sum(f => f.Price);
            var decorationsValue = aquarium.Decorations.Sum(d => d.Price);
            var result = fishValue + decorationsValue;

            return string.Format(OutputMessages.AquariumValue, aquariumName, Math.Round(result, 2));
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var tank in aquariums)
            {
                sb.Append(tank.GetInfo() + Environment.NewLine);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
