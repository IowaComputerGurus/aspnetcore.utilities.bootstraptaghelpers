using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Diffing;
using VerifyTests;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests;

public static class Initializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifyAngleSharpDiffing.Initialize();

        // Shove all the verify files into a custom directory
        VerifierSettings.DerivePathInfo((file, directory, type, method) => new(directory: Path.Combine(directory, "VerifySnapshots"), type.Name, method.Name));
        
        // Automatically "verify" tests on the first run
        
        VerifierSettings.OnFirstVerify(pair =>
        {
            File.Move(pair.ReceivedPath, pair.VerifiedPath);
            return Task.CompletedTask;
        });
    }
}

