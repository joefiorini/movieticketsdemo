$routes.map_route "theaters-by-zipcode", "theaters/zipcode/{zip}",
  { :controller => 'Theaters', :action => 'Show', :zip => '00000' },
  {:zip => "\\d{5}(-\\d{4}$|$)"}

$routes.map_route "delete-movie", "movies/delete/{id}",
  {:controller => 'Movies', :action => 'delete' }

$routes.map_route "movies/create",
  {:controller => 'Movies', :action => 'create' }

$routes.map_route "movies/update",
  {:controller => 'Movies', :action => 'update' }
  
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

