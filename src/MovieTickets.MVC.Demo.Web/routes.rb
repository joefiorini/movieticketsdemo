include MovieTickets::MVC::Demo::Web::RouteConstraints

$routes.map_route "movies/create",
  {:controller => 'Movies', :action => 'create' }
  
$routes.map_route "edit-movie", "movies/{id}/edit",
  {:controller => 'Movies', :action => 'Edit' }
  
$routes.map_route "new-movie", "movies/new",
  {:controller => 'Movies', :action => 'New' }

$routes.map_route "Movie", "movies/{id}",
  {:controller => 'Movies', :action => 'Show' }

$routes.map_route "Movies", "movies",
  {:controller => 'Movies', :action => 'Index' }

$routes.map_route "{controller}/{action}"
  {:controller => 'Home', :action => 'index' }
$routes.map_route "{controller}/{action}/{id}",
  {:controller => 'Home', :action => 'index', :id => ''}

