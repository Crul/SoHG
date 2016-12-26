using Sohg.TechnologyAgg.Contracts;
using System.Linq;

namespace Sohg.GameAgg
{
    public partial class GameEngine
    {
        public bool CheckDependentTechnologies(ITechnologyDependent technologyDependent)
        {
            var activeTechnologies = GameDefinition.TechnologyCategories.SelectMany
                (
                    technologyCategory => technologyCategory.Technologies
                        .Where(technology => technology.IsActive)
                );

            return technologyDependent.RequiredTechnologies
                .All(requiredTechnology => activeTechnologies.Contains(requiredTechnology));
        }
        
        public void OnTechnologyActivated()
        {
            GameDefinition.SocietyActions.ToList()
                .ForEach(action => action.CheckActivation());

            GameDefinition.Skills.ToList()
                .ForEach(skill => skill.CheckActivation());
        }
    }
}
