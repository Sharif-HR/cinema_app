namespace CinemaApp {
    using System;
    using System.IO;

    public static class DotEnv {
        public static void Load(string filePath) {
            if(!File.Exists(filePath)) {
                return;
            }

            foreach (var line in File.ReadLines(filePath)) {
                // Create an array based of the = in the .env file
                var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if(parts.Length != 2) {
                    continue;
                }

                // Set the local env variable
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}
