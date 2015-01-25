﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using LiveResults.Client.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiveResults.Client.Tests
{
    [TestClass]
    public class IofXmlParserTests
    {
        [TestMethod]
        public void ParseIofV2XmlFile()
        {
            var runners = Parsers.IOFXmlV2Parser.ParseFile(TestHelpers.GetPathToTestFile("20130508_200904_emma.xml"),
                delegate(string msg)
                {
                }, false,
               new IOFXmlV2Parser.IDCalculator(0).CalculateID);
                                                                           

            Assert.AreEqual(377, runners.Length);

            Assert.AreEqual("Agnë Juodagalvytë", runners[0].Name);
            Assert.AreEqual("Àþuolas OK", runners[0].Club);
            Assert.AreEqual("S14", runners[0].Class);
            Assert.AreEqual(100400, runners[0].Time);
            Assert.AreEqual(0, runners[0].Status);
            Assert.AreEqual(1, runners[0].SplitTimes.Length);
            Assert.AreEqual(1043, runners[0].SplitTimes[0].Control);
            Assert.AreEqual(49100, runners[0].SplitTimes[0].Time);
        }

        [TestMethod]
        public void TestFinishPunchDetected()
        {
            var runners = IOFXmlV2Parser.ParseFile(TestHelpers.GetPathToTestFile("splitsResult_Kugler_Johann_in_Finish.xml"),
                delegate(string msg)
                {
                }, false,
               new IOFXmlV2Parser.IDCalculator(0).CalculateID);


            var runner = runners.First(x => x.ID == 12208);
            Assert.AreEqual(0, runner.Status);
            Assert.AreEqual(107600, runner.Time);
            
        }

        [TestMethod]
        public void VerifyIofV2XmlFileNotCompetingDoesNotExis()
        {
            var runners = Parsers.IOFXmlV2Parser.ParseFile(TestHelpers.GetPathToTestFile("iof_xml_notcompeting.xml"),
                new LogMessageDelegate(delegate(string msg)
                {
                }), false,
               new IOFXmlV2Parser.IDCalculator(0).CalculateID);

            Assert.IsNull(runners.FirstOrDefault(x => x.Name == "Stepan Malinovskii"));
        }
    }
}