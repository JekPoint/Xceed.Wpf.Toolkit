﻿/*************************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System;
using System.Collections.Specialized;
using System.Windows;

namespace Xceed.Wpf.DataGrid
{
  internal sealed class ProxySortDescriptionsChangedEventManager : WeakEventManager
  {
    #region Constructor

    private ProxySortDescriptionsChangedEventManager()
    {
    }

    #endregion

    #region CurrentManager Private Property

    private static ProxySortDescriptionsChangedEventManager CurrentManager
    {
      get
      {
        var managerType = typeof( ProxySortDescriptionsChangedEventManager );
        var currentManager = ( ProxySortDescriptionsChangedEventManager )WeakEventManager.GetCurrentManager( managerType );

        if( currentManager == null )
        {
          currentManager = new ProxySortDescriptionsChangedEventManager();
          WeakEventManager.SetCurrentManager( managerType, currentManager );
        }

        return currentManager;
      }
    }

    #endregion

    public static void AddListener( DataGridCollectionViewBase source, IWeakEventListener listener )
    {
      if( source == null )
        throw new ArgumentNullException( "source" );

      if( listener == null )
        throw new ArgumentNullException( "listener" );

      CurrentManager.ProtectedAddListener( source, listener );
    }

    public static void RemoveListener( DataGridCollectionViewBase source, IWeakEventListener listener )
    {
      if( source == null )
        throw new ArgumentNullException( "source" );

      if( listener == null )
        throw new ArgumentNullException( "listener" );

      CurrentManager.ProtectedRemoveListener( source, listener );
    }

    protected override void StartListening( object source )
    {
      var target = ( DataGridCollectionViewBase )source;
      target.ProxySortDescriptionsChanged += new NotifyCollectionChangedEventHandler( this.OnEventRaised );
    }

    protected override void StopListening( object source )
    {
      var target = ( DataGridCollectionViewBase )source;
      target.ProxySortDescriptionsChanged -= new NotifyCollectionChangedEventHandler( this.OnEventRaised );
    }

    private void OnEventRaised( object sender, NotifyCollectionChangedEventArgs e )
    {
      this.DeliverEvent( sender, e );
    }
  }
}
