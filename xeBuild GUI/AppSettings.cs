//
// AppSettings.cs
// xeBuild GUI
//
// Created by Swizzy 18/07/2015
// Copyright (c) 2015 Swizzy. All rights reserved.

using System.Collections.Generic;
using System.Windows.Documents;

namespace xeBuild_GUI {

    internal class AppSettings {
        private OptionalPatch[] Patches;
        public UserModes Mode;

        internal AppSettings() {
            Mode = UserModes.Simple;
            Patches = new[] {
                                new OptionalPatch("Patch 1"),
                                new OptionalPatch("Patch 2"),
                                new OptionalPatch("Patch 3"),
                                new OptionalPatch("Patch 4"),
                                new OptionalPatch("Patch 5")
                            };
        }

        public OptionalPatch[] GetAvailablePatches() {
            return Patches;
        }

        internal enum UserModes {
            Simple,
            Advanced
        }
    }

    internal class OptionalPatch {
        public string Description;
        public string DisplayName;
        public string Name;
        public List<string> Versions = new List<string>();

        public OptionalPatch(string displayName, string name = "", string description = "") {
            Name = name;
            DisplayName = displayName;
            Description = description;
        }

        public bool IsUsed { get; set; }
        public override string ToString() { return DisplayName; }
    }

}