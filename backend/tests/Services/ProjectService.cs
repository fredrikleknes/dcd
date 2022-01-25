using System;
using System.Collections.Generic;
using System.Linq;

using api.Models;
using api.SampleData.Generators;
using api.Services;

using Xunit;


namespace tests
{
    [Collection("Database collection")]
    public class ProjectServiceTest
    {
        DatabaseFixture fixture;

        public ProjectServiceTest(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }
        [Fact]
        public void GetAll()
        {
            var projectFromSampleDataGenerator = SampleCaseGenerator.initializeCases(SampleAssetGenerator.initializeAssets()).Projects.OrderBy(p => p.ProjectName);
            ProjectService projectService = new ProjectService(fixture.context);
            var projectsFromService = projectService.GetAll().OrderBy(p => p.ProjectName);
            var projectsExpectedActual = projectFromSampleDataGenerator.Zip(projectsFromService);
            Assert.Equal(projectFromSampleDataGenerator.Count(), projectsFromService.Count());
            foreach (var projectPair in projectsExpectedActual)
            {
                compareProjects(projectPair.First, projectPair.Second);
            }
        }

        [Fact]
        public void GetProject()
        {
            ProjectService projectService = new ProjectService(fixture.context);
            IEnumerable<Project> projectsFromGetAllService = projectService.GetAll();
            var projectsFromSampleDataGenerator = SampleCaseGenerator.initializeCases(SampleAssetGenerator.initializeAssets()).Projects;
            Assert.Equal(projectsFromSampleDataGenerator.Count(), projectsFromGetAllService.Count());
            foreach (var project in projectsFromGetAllService)
            {
                var projectFromGetProjectService = projectService.GetProject(project.Id);
                var projectFromSampleDataGenerator = projectsFromSampleDataGenerator.Find(p => p.ProjectName == project.ProjectName);
                compareProjects(projectFromSampleDataGenerator, projectFromGetProjectService);
            }
        }
        void compareProjects(Project expected, Project actual)
        {
            Assert.Equal(expected.ProjectName, actual.ProjectName);
            Assert.Equal(expected.ProjectPhase, actual.ProjectPhase);
            Assert.Equal(expected.ProjectCategory, actual.ProjectCategory);
            Assert.Equal(expected.Cases.Count(), actual.Cases.Count());
            var casesSourceAndTarget = expected.Cases.OrderBy(c => c.Name).Zip(actual.Cases.OrderBy(c => c.Name));
            foreach (var casePair in casesSourceAndTarget)
            {
                compareCases(casePair.First, casePair.Second);
            }
            Assert.Equal(expected.DrainageStrategies.Count(), actual.DrainageStrategies.Count());
            var drainageStrategiesExpectedAndActual = expected.DrainageStrategies.OrderBy(d => d.Name)
                .Zip(actual.DrainageStrategies.OrderBy(d => d.Name));
            foreach (var drainageStrategyPair in drainageStrategiesExpectedAndActual)
            {
                compareDrainageStrategies(drainageStrategyPair.First, drainageStrategyPair.Second);
            }
            var wellProjectsExpectedAndActual = expected.WellProjects.OrderBy(d => d.Name)
                .Zip(actual.WellProjects.OrderBy(d => d.Name));
            foreach (var wellProjectPair in wellProjectsExpectedAndActual)
            {
                compareWellProjects(wellProjectPair.First, wellProjectPair.Second);
            }
            var explorationsExpectedAndActual = expected.Explorations.OrderBy(d => d.Name)
                .Zip(actual.Explorations.OrderBy(d => d.Name));
            foreach (var explorationPair in explorationsExpectedAndActual)
            {
                compareExplorations(explorationPair.First, explorationPair.Second);
            }
        }

        void compareCases(Case expected, Case actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.ReferenceCase, actual.ReferenceCase);
            Assert.Equal(expected.ProducerCount, actual.ProducerCount);
            Assert.Equal(expected.GasInjectorCount, actual.GasInjectorCount);
            Assert.Equal(expected.WaterInjectorCount, actual.WaterInjectorCount);
            Assert.Equal(expected.RiserCount, actual.RiserCount);
            Assert.Equal(expected.TemplateCount, actual.TemplateCount);
            Assert.Equal(expected.FacilitiesAvailability, actual.FacilitiesAvailability);
            Assert.Equal(expected.ArtificialLift, actual.ArtificialLift);
            compareCosts(expected.CessationCost, actual.CessationCost);
        }

        void compareCosts<T>(TimeSeriesCost<T> expected, TimeSeriesCost<T> actual)
        {
            if (expected == null || actual == null)
            {
                Assert.Equal(expected, actual);
            }
            else
            {
                compareYearValues(expected, actual);
                Assert.Equal(expected.Currency, actual.Currency);
            }
        }
        void compareDrainageStrategies(DrainageStrategy expected, DrainageStrategy actual)
        {
            if (expected == null || actual == null)
            {
                Assert.Equal(expected, actual);
            }
            else
            {
                Assert.Equal(expected.NGLYield, actual.NGLYield);
                Assert.Equal(expected.Name, actual.Name);
                compareVolumes(expected.ProductionProfileOil, actual.ProductionProfileOil);
                compareVolumes(expected.ProductionProfileGas, actual.ProductionProfileGas);
                compareVolumes(expected.ProductionProfileWater, actual.ProductionProfileWater);
                compareVolumes(expected.ProductionProfileWaterInjection, actual.ProductionProfileWaterInjection);
                compareVolumes(expected.NetSalesGas, actual.NetSalesGas);
                compareMasses(expected.Co2Emissions, actual.Co2Emissions);
            }
        }

        void compareWellProjects(WellProject expected, WellProject actual)
        {
            if (expected == null || actual == null)
            {
                Assert.Equal(expected, actual);
            }
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.WellType, actual.WellType);
            Assert.Equal(expected.RigMobDemob, actual.RigMobDemob);
            Assert.Equal(expected.AnnualWellInterventionCost, actual.AnnualWellInterventionCost);
            Assert.Equal(expected.PluggingAndAbandonment, actual.PluggingAndAbandonment);
            compareYearValues(expected.DrillingSchedule, actual.DrillingSchedule);
            compareCosts(expected.CostProfile, actual.CostProfile);
        }

        void compareExplorations(Exploration expected, Exploration actual)
        {
            if (expected == null || actual == null)
            {
                Assert.Equal(expected, actual);
            }
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.WellType, actual.WellType);
            Assert.Equal(expected.RigMobDemob, actual.RigMobDemob);
            compareCosts(expected.CostProfile, actual.CostProfile);
            compareYearValues(expected.DrillingSchedule, actual.DrillingSchedule);
            compareCosts(expected.GAndGAdminCost, actual.GAndGAdminCost);
        }

        void compareVolumes<T>(TimeSeriesVolume<T> expected, TimeSeriesVolume<T> actual)
        {
            if (expected == null || actual == null)
            {
                Assert.Equal(expected, actual);
            }
            else
            {
                compareYearValues(expected, actual);
                Assert.Equal(expected.Unit, actual.Unit);
            }
        }
        void compareMasses<T>(TimeSeriesMass<T> expected, TimeSeriesMass<T> actual)
        {
            if (expected == null || actual == null)
            {
                Assert.Equal(expected, actual);
            }
            else
            {
                compareYearValues(expected, actual);
                Assert.Equal(expected.Unit, actual.Unit);
            }
        }
        void compareYearValues<T>(TimeSeriesBase<T> expected, TimeSeriesBase<T> actual)
        {
            if (expected == null || actual == null)
            {
                Assert.Equal(expected, actual);
            }
            else
            {
                Assert.Equal(expected.YearValues.Count(), actual.YearValues.Count());
                var yearValuePairsXY = expected.YearValues.OrderBy(v => v.Year).Zip(actual.YearValues.OrderBy(v => v.Year));
                foreach (var pair in yearValuePairsXY)
                {
                    Assert.Equal(pair.First.Year, pair.Second.Year);
                    Assert.Equal(pair.First.Value, pair.Second.Value);
                }
            }
        }
        [Fact]
        public void GetDoesNotExist()
        {
            ProjectService projectService = new ProjectService(fixture.context);
            Assert.Throws<NotFoundInDBException>(() => projectService.GetProject(new Guid()));
        }
    }
}
