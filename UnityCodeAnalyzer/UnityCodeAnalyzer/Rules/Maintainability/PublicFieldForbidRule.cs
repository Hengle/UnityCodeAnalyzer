using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;


namespace UnityCodeAnalyzer.Rules
{
    public class PublicFieldForbidRule
    {
        public static void Init(AnalysisContext context)
        {
            context.RegisterSymbolAction(Analyze, SymbolKind.Field);
        }

        private static void Analyze(SymbolAnalysisContext context)
        {
            var fieldSymbol = (IFieldSymbol)context.Symbol;
            if (fieldSymbol.DeclaredAccessibility == Accessibility.Public)
            {
                var diagnostic = Diagnostic.Create(Descriptor, fieldSymbol.Locations[0]);
                context.ReportDiagnostic(diagnostic);
            }
        }

        public const string ID = "PublicFieldForbidRule";
        public static DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(ID, Title, Message, Category.ToString(), Severity, true);
        public static string Title => "Public field is forbidden.";
        public static string Message => "Public field is forbidden. use public property instead.";
        public static DiagnosticsCategory Category => DiagnosticsCategory.Maintainability;
        public static DiagnosticSeverity Severity => DiagnosticSeverity.Error;

        /// <summary>
        /// keep all the code of a method in screen.
        /// </summary>
        public const int ExceptedMethodLine = 60;
        }
    }
