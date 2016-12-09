using Sohg.TechnologyAgg.Contracts;
using System.Linq;

namespace Sohg.GameAgg
{
    public partial class GameEngine
    {
        public bool CheckDependentTechnologies(ITechnologyDependent technologyDependent)
        {
            var activeTechnologies = gameDefinition.TechnologyCategories.SelectMany
                (
                    technologyCategory => technologyCategory.Technologies
                        .Where(technology => technology.IsActive)
                );

            return technologyDependent.RequiredTechnologies
                .All(requiredTechnology => activeTechnologies.Contains(requiredTechnology));
        }
        
        public void OnTechnologyActivated()
        {
            gameDefinition.SocietyActions.ToList()
                .ForEach(action => action.CheckActivation());

            gameDefinition.Skills.ToList()
                .ForEach(skill => skill.CheckActivation());
        }
    }
}
