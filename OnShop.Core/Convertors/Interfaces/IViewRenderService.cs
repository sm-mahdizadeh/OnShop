﻿namespace OnShop.Core.Convertors.Interfaces
{
    public interface IViewRenderService
    {
        string RenderToStringAsync(string viewName, object model);
    }
}