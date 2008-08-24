$routes.map_route "{controller}/{action}/{id}",
  {:controller => 'Home', :action => 'index', :id => ''}

$routes.map_route "Movie", "movie/{id}",
  {:controller => 'Movies', :action => 'show' }
