﻿using ExtremeSkins.Converter.Core.Analyzer;

namespace ExtremeSkins.Converter.Core.Interface;

public interface IRepositoryAnalyzer
{
    public string Name { get; }
    public string TargetPath { get; }

    public bool IsValid();

    public AnalyzeResult Analyze();
}
