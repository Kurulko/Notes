﻿namespace Notes.ViewModels;

public class IndexViewModel<T>
{
    public IEnumerable<T> Models { get; }
    public PageViewModel PageViewModel { get; }
    public IndexViewModel(IEnumerable<T> models, PageViewModel viewModel)
    {
        Models = models;
        PageViewModel = viewModel;
    }
}