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

: field-slice
   model!
   disp-image
   iheight 0 do
     iwidth 0 do
       i j 0 point!
       distance 2* 2* 2*
       dup >r dup >r
       >byte over c! 1+
       r> negate >byte over c! 1+
       r> dup negate >byte swap 0= or over c! 1+
       255 over c! 1+
     loop
   loop
   drop
   blt-image
;

set-current
( public )

: field-slice field-slice ;

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

previous
