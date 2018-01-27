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

: model
  gpush
    128 128 0 translated
    50 sphere
  gpop
  gpush
    180 100 0 translated
    30 30 30 box
  gpop
  difference
;
' model set-model

: field-slice
   disp-image
   iheight 0 do
     iwidth 0 do
       i j 0 set-point
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

previous
