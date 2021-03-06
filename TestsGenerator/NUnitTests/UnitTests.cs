using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestsGenerator;

namespace NUnitTests
{
    [TestFixture]
    public class Tests
    {
        string fileCode;

        private List<TestStructure> generatedTests = null;

        private SyntaxNode classOneRoot;
        private SyntaxNode classTwoRoot;

        [SetUp]
        public void Setup()
        {
            string filePath = "..\\..\\..\\TwoClasses.cs";

            fileCode = File.ReadAllText(filePath);

            generatedTests = TestCreator.Generate(fileCode).Result;

            classOneRoot = CSharpSyntaxTree.ParseText(generatedTests[0].TestCode).GetRoot();
            classTwoRoot = CSharpSyntaxTree.ParseText(generatedTests[1].TestCode).GetRoot();
        }

        //?????????? ??????????????? ?????? (?? ???? TwoClasses -> 2 ?????)
        [Test]
        public void AmountOfClassesInListTests()
        {
            Assert.AreEqual(generatedTests.Count,2);
        }        

        //?????????? ??????????? ? ???????? ????
        [Test]
        public void AmountOfNamespacesTest()
        {
            int namespacesOneCount = classOneRoot.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Count();
            Assert.AreEqual(namespacesOneCount, 1);
        }

        [Test]
        public void NameOfTestFiles() 
        {
            Assert.AreEqual(generatedTests[0].TestName, "FirstClassTests.cs");//classname+tests+.cs
            Assert.AreEqual(generatedTests[1].TestName, "SecondClassTests.cs");           
        }

        [Test]
        public void UsingDirectivesTest()
        {
            string usings ="using System;\r\n" +
                "using System.Linq;\r\n" +
                "using System.Collections.Generic;\r\n" +
                "using NUnit.Framework;\r\n" +
                "using NUnitTests;\r\n";
            Assert.IsTrue(generatedTests[0].TestCode.Contains(usings));
        }

        [Test]
        public void MethodsNamesTest()
        {
            MethodDeclarationSyntax method = classOneRoot.DescendantNodes().OfType<MethodDeclarationSyntax>().First();
            Assert.IsTrue(method.Identifier.ToString() == "HiTest");
        }

        [Test]
        public void MethodsBodyTest()
        {
            BlockSyntax methodBody = classOneRoot.DescendantNodes().OfType<MethodDeclarationSyntax>().First().Body;
            string  mb = methodBody.ToString();
            Assert.IsTrue(mb.Contains("Assert.Fail(\"auto\")"));

        }
    }
}