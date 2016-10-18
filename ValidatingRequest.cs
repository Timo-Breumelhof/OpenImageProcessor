﻿using DotNetNuke.Web.Api;
using ImageProcessor.Web.HttpModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Satrabel.OpenImageProcessor
{
    public class ValidatingRequest : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            ImageProcessingModule.ValidatingRequest += (sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(args.QueryString))
                {
                    var queryCollection = HttpUtility.ParseQueryString(args.QueryString);
                    // ignore DNN cachebuster
                    if (queryCollection.AllKeys.Contains("ver"))
                    {
                        queryCollection.Remove("ver");
                    }
                    // ignore ckeditor cachebuster
                    if (queryCollection.AllKeys.Contains("t"))
                    {
                        queryCollection.Remove("t");
                    }
                    args.QueryString = queryCollection.ToString();
                }
            }; 
        }
    }
}