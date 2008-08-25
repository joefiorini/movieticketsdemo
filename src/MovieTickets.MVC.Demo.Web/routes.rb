include MovieTickets::MVC::Demo::Web::RouteConstraints

$routes.map_route "Movie", "movies/{id}",
  {:controller => 'Movies', :action => 'Show' }

$routes.map_route "{controller}/{action}/{id}",
  {:controller => 'Home', :action => 'index', :id => ''}

