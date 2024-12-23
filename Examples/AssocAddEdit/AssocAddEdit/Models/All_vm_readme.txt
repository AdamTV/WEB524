
This text explains the design of the View Model Classes...

Remember that view model classes are "packaging containers" for data, as defined by use cases.

Yes, they are based on design model classes, but it is NOT necessary to exactly match them. 

Therefore, let's look at the view model classes:

CountryBaseViewModel
===========
Display only. 
Not connected with any other view model class.


ManufacturerBaseViewModel
================
Display only.
Used as the base class for "with details" (described next).


ManufacturerWithDetailsViewModel
=======================
Display only.
Adds a composed property name that will hold the country name string.
Also has a collection property for its vehicles - points to VehicleBaseViewModel.


VehicleAddViewModel
==========
Handles the data the user sends for the "add new" use case.
Used as a base class for "base" (described next).


VehicleAddFormViewModel
==============
Packaging for the data that's needed for the "add new" use case.
Includes a SelectList object.
Does inherit from VehicleAddViewModel, just to save coding effort.


VehicleBaseViewModel
===========
Used as a base class for "with details" (described next).


VehicleWithDetailsViewModel
==================
Display only.
Adds composed property names for data from a chain! of associated objects.


VehicleEditFormViewModel
===============
Packaging for the data that's needed for the "edit existing" use case.
Does NOT inherit from any other class.

VehicleEditViewModel
===========
Handles the data the user sends for the "edit existing" use case.
