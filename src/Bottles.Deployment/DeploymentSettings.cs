using System;
using Bottles.Deployment.Configuration;
using Bottles.Deployment.Parsing;
using FubuCore;
using FubuCore.Configuration;

namespace Bottles.Deployment
{
    public class DeploymentSettings
    {
        //path points to ~/deployment
        public DeploymentSettings(string path)
        {
            DeploymentDirectory = path;
            BottlesDirectory = FileSystem.Combine(path, ProfileFiles.BottlesDirectory);
            RecipesDirectory = FileSystem.Combine(path, ProfileFiles.RecipesDirectory);
            EnvironmentFile = FileSystem.Combine(path, EnvironmentSettings.EnvironmentSettingsFileName);
            TargetDirectory = FileSystem.Combine(path, ProfileFiles.TargetDirectory);
            BottleManifestFile = FileSystem.Combine(path, ProfileFiles.BottlesManifestFile);
            EnvironmentsDirectory = FileSystem.Combine(path, ProfileFiles.EnvironmentsDirectory);
            ProfilesDirectory = FileSystem.Combine(path, ProfileFiles.ProfilesDirectory);
            DeployersDirectory = FileSystem.Combine(path, ProfileFiles.DeployersDirectory);
        }

        public DeploymentSettings() : this(".".ToFullPath())
        {
        }

        public string DeployersDirectory { get; set; }

        public string DeploymentDirectory { get; set; }
        public string TargetDirectory { get; set; }
        public string BottlesDirectory { get; set; }
        public string RecipesDirectory { get; set; }
        public string EnvironmentFile { get; set; }
        public string BottleManifestFile { get; set; }
        public string EnvironmentsDirectory { get; set; }
        public string ProfilesDirectory { get; set; }

        public EnvironmentSettings Environment { get; set; }
        public Profile Profile { get; set; }

        public string StagingDirectory
        {
            get { return FileSystem.Combine(TargetDirectory, ProfileFiles.StagingDirectory); }
        }

        public DeploymentPlan Plan { get; set; }

        public static DeploymentSettings ForDirectory(string directory)
        {
            if (directory.IsNotEmpty())
            {
                return new DeploymentSettings(directory);
            }

            var path = FileSystem.Combine(".".ToFullPath(), ProfileFiles.DeploymentFolder);
            return new DeploymentSettings(path);
        }


        public virtual IKeyValues SubstitutionValues()
        {
            return new SettingsRequestData(Plan.Substitutions());
        }


        public string GetRecipeDirectory(string recipe)
        {
            return FileSystem.Combine(RecipesDirectory, recipe);
        }

        public string GetHost(string recipe, string host)
        {
            var p = GetRecipeDirectory(recipe);

            //TODO: harden
            p = FileSystem.Combine(p, host + ".host");

            return p;
        }

        public string GetProfile(string profileName)
        {
            return FileSystem.Combine(ProfilesDirectory, profileName + "." + ProfileFiles.ProfileSuffix);
        }
    }
}