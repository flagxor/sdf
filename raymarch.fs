get-current
vocabulary raymarch also raymarch definitions
( private )

variable model   :noname ; model !
variable viewer   :noname ; viewer !

2 constant epsilon

: 3dup ( a b c -- a b c a b c ) >r 2dup r> dup >r -rot r> ;
: distance ( -- n ) model @ execute ;
: offsetdist gpush translated distance gpop ;
: delta ( a b c -- dx )
    3dup negate rot negate rot negate rot offsetdist >r offsetdist r> - ;
: origin 0 0 0 ;

variable cx   variable cy   variable cz

set-current
( public )

10000 constant march-scale
march-scale 2/ constant hmarch
10000 100 * constant mega-scale

: model! ( model -- ) model ! ;
: camerapix! ( x y -- ) hmarch - negate cy ! hmarch - cx ! ;
: distance ( -- n ) distance ;

: gradient ( -- x y z )
  epsilon 0 0 delta   0 epsilon 0 delta   0 0 epsilon delta ;

: march
   march-scale negate cz !
   30 0 do cx @ cy @ cz @ point!
      distance cz +! cz @ mega-scale min cz ! loop ;

previous

