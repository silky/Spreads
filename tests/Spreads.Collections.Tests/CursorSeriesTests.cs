﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Runtime.InteropServices;
using Spreads.Collections;
using System.Diagnostics;

namespace Spreads.Collections.Tests {
    [TestFixture]
    public class CursorSeriesTests {
        [Test]
        public void CouldCalculateAverageOnMovingWindow() {
            var sm = new SortedMap<DateTime, double>();

            var count = 1000000;

            for (int i = 0; i < count; i++) {
                sm.Add(DateTime.UtcNow.Date.AddSeconds(i), i);
            }

            // slow implementation
            var sw = new Stopwatch();
            sw.Start();
            var ma = sm.Window(20, 1);
            var c = 1;
            foreach (var m in ma) {
                var innersm = m.Value;
                if (innersm.Values.Average() != c + 8.5) {
                    Console.WriteLine(m.Value);
                    throw new ApplicationException("Invalid value");
                }
                c++;
            }
            sw.Stop();
            Console.WriteLine("Window MA, elapsed: {0}, ops: {1}", sw.ElapsedMilliseconds, (int)((double)count / (sw.ElapsedMilliseconds / 1000.0)));

        }
    }
}
