get-current
vocabulary raymarch also raymarch definitions
( private )

2 constant epsilon

: 3dup ( a b c -- a b c a b c ) >r 2dup r> dup >r -rot r> ;
: offsetdist gpush translated distance gpop ;
: delta ( a b c -- dx )
    3dup negate rot negate rot negate rot offsetdist >r offsetdist r> - ;

set-current
( public )

: cast ( x y -- ) ;
: march ( model -- )
   0 0 0 set-point
   20 0 do dup execute >r 0 0 r> translated loop drop
;

: gradient ( model -- x y z )
  epsilon 0 0 delta   0 epsilon 0 delta   0 0 epsilon delta ;

previous

