get-current
vocabulary sdf also sdf definitions
( private )

variable x   variable y   variable z
variable opposite   variable adjacent   variable hypotenuse
variable model

20 constant stack-slots
create gstack stack-slots cells 3 * allot   variable gsp gstack gsp !
: >g ( n -- ) gsp @ ! cell gsp +! ;
: g> ( -- n ) -1 cells gsp +! gsp @ @ ;

: -! ( n a -- ) swap negate swap +! ;
: square ( n -- n2 ) dup * ;
: sqrt ( n -- n1/2 ) s>f fsqrt f>s ;

: anglize ( a b -- )
    2dup square swap square + hypotenuse ! opposite ! adjacent ! ;
: sin* ( n -- n ) opposite @ hypotenuse @ */ ;
: cos* ( n -- n ) adjacent @ hypotenuse @ */ ;

set-current
( public )

: point! ( x y z -- ) z ! y ! x ! ;
: model! ( model -- ) model ! ;
: distance ( -- n ) model @ execute ;

: sphere ( r -- d )
    x @ square y @ square + z @ square + sqrt swap - ;
: box ( x y z -- d )
    z @ abs swap - swap y @ abs swap - max swap x @ abs swap - max ;
: cylinder ( h r -- d )
    x @ square y @ square + sqrt swap - swap z @ abs swap - max ;

: gpush   x @ >g y @ >g z @ >g ;
: gpop   g> z ! g> y ! g> x ! ;

: translated ( x y z -- ) z -! y -! x -! ;
: rotate-z ( x y -- ) anglize x @ cos* y @ sin* - x @ sin* y @ cos* + y ! x ! ;
: rotate-y ( x z -- ) anglize x @ cos* z @ sin* - x @ sin* z @ cos* + z ! x ! ;

: union ( a b -- d ) min ;
: intersect ( a b -- d ) max ;
: difference ( a -b -- d ) negate max ;

previous

