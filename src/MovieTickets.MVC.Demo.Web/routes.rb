include MovieTickets::MVC::Demo::Web::RouteConstraints

$routes.map_route "Movie", "movies/{id}",
  {:controller => 'Movies', :action => 'Show' }

$routes.map_route "Movies", "movies",
  {:controller => 'Movies', :action => 'Index' }

$routes.map_route "{controller}/{action}/{id}",
  {:controller => 'Home', :action => 'index', :id => ''}

