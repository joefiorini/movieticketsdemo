$routes.map_route "{controller}/{action}/{id}",
  {:controller => 'Home', :action => 'index', :id => ''}

$routes.map_route "Movie", "movies/{id}",
  {:controller => 'Movies', :action => 'show' }
