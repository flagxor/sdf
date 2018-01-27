#! /usr/bin/env gforth

require simplefont/grf.fs
require sdf.fs
require raymarch.fs

get-current
vocabulary samples also samples definitions
( private )

256 constant iwidth
256 constant iheight
iwidth iheight * 4 * constant image-size

create disp-image image-size allot

: pheight height 3 4 */ ;

: blt-image
   pheight 0 do
     width pheight - 2/  height pheight - 2/ i + pixel
     pheight 0 do
       i iwidth pheight */ j iheight pheight */
       iwidth * + 4 * disp-image + over 4 cmove
       4 +
     loop
     drop
   loop
;

: >byte 255 min 0 max ;
: color-code ( a n -- a )
   dup >r dup >r
   >byte over c! 1+
   r> negate >byte over c! 1+
   r> dup negate >byte swap 0= or over c! 1+
   255 over c! 1+
;

: field-slice
   model!
   disp-image
   iheight 0 do
     iwidth 0 do
       i iheight 1- j - 0 point!
       distance 2* 2* 2*
       color-code
     loop
   loop
   drop
   blt-image
;

: raymarch-model
   model!
   disp-image
   iheight 0 do
     iwidth 0 do
       i iwidth iheight - 2/ + march-scale iheight */
       j march-scale iheight */ camerapix!
       march point-distance 255 march-scale */
       color-code
     loop
   loop
   drop
   blt-image
;

set-current
( public )

: field-slice field-slice ;
: raymarch-model raymarch-model ;

: sphere-model
  gpush
    128 128 0 translated
    50 sphere
  gpop
;

: box-model
  gpush
    128 128 0 translated
    50 50 50 box
  gpop
;

: cylinder-model
  gpush
    128 128 0 translated
    50 50 cylinder
  gpop
;

: sphere-inverse-model
  gpush
    128 128 0 translated
    50 sphere
  gpop
  negate
;

: difference-model
  gpush
    128 128 0 translated
    50 sphere
  gpop
  gpush
    128 128 0 translated
    30 sphere
  gpop
  difference
;

: union-model
  gpush
    128 128 0 translated
    50 sphere
  gpop
  gpush
    180 128 0 translated
    50 sphere
  gpop
  union
;

: intersect-model
  gpush
    128 128 0 translated
    50 sphere
  gpop
  gpush
    180 128 0 translated
    50 sphere
  gpop
  intersect
;

: smiley-model
  gpush
    128 128 0 translated 80 sphere
    gpush
      20 forward
      gpush
        30 left 20 sphere difference
      gpop
      gpush
        30 right 20 sphere difference
      gpop
    gpop
    gpush
      30 back 50 10 10 box difference
    gpop
  gpop
;

: smiley2-model
  gpush
    8000 up
    3200 sphere
    gpush
      800 forward
      gpush
        1200 left 4000 800 cylinder difference
      gpop
      gpush
        1200 right 4000 800 cylinder difference
      gpop
    gpop
    gpush
      1200 back 2000 400 8000 box difference
    gpop
  gpop
;

previous
