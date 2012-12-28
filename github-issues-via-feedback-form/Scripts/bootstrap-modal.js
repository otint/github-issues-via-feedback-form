/* =========================================================
* bootstrap-modal.js v1.4.0
* http://twitter.github.com/bootstrap/javascript.html#modal
* =========================================================
* Copyright 2011 Twitter, Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
* ========================================================= */


!function ($) {

  "use strict"

  /* CSS TRANSITION SUPPORT (https://gist.github.com/373874)
  * ======================================================= */

  var transitionEnd

  $(document).ready(function () {

    $.support.transition = (function () {
      var thisBody = document.body || document.documentElement
        , thisStyle = thisBody.style
        , support = thisStyle.transition !== undefined || thisStyle.WebkitTransition !== undefined || thisStyle.MozTransition !== undefined || thisStyle.MsTransition !== undefined || thisStyle.OTransition !== undefined
      return support
    })()

    // set CSS transition event type
    if ($.support.transition) {
      transitionEnd = "TransitionEnd"
      if ($.browser.webkit) {
        transitionEnd = "webkitTransitionEnd"
      } else if ($.browser.mozilla) {
        transitionEnd = "transitionend"
      } else if ($.browser.opera) {
        transitionEnd = "oTransitionEnd"
      }
    }

  })


  /* MODAL PUBLIC CLASS DEFINITION
  * ============================= */

  var Modal = function (content, options) {
    this.settings = $.extend({}, $.fn.modal.defaults, options)
    this.$element = $(content)
      .delegate('.close', 'click.modal', $.proxy(this.hide, this))

    if (this.settings.show) {
      this.show()
    }

    return this
  }

  Modal.prototype = {

    toggle: function () {
      return this[!this.isShown ? 'show' : 'hide']()
    }

    , show: function () {
      var that = this
      this.isShown = true
      this.$element.trigger('show')

      escape.call(this)
      backdrop.call(this, function () {
        var transition = $.support.transition && that.$element.hasClass('fade')

        that.$element
            .appendTo(document.body)
            .show()

        if (transition) {
          that.$element[0].offsetWidth // force reflow
        }

        that.$element.addClass('in')

        transition ?
            that.$element.one(transitionEnd, function () { that.$element.trigger('shown') }) :
            that.$element.trigger('shown')

      })

      return this
    }

    , hide: function (e) {
      e && e.preventDefault()

      if (!this.isShown) {
        return this
      }

      var that = this
      this.isShown = false

      escape.call(this)

      this.$element
          .trigger('hide')
          .removeClass('in')

      $.support.transition && this.$element.hasClass('fade') ?
          hideWithTransition.call(this) :
          hideModal.call(this)

      return this
    }

  }


  /* MODAL PRIVATE METHODS
  * ===================== */

  function hideWithTransition() {
    // firefox drops transitionEnd events :{o
    var that = this
      , timeout = setTimeout(function () {
        that.$element.unbind(transitionEnd)
        hideModal.call(that)
      }, 500)

    this.$element.one(transitionEnd, function () {
      clearTimeout(timeout)
      hideModal.call(that)
    })
  }

  function hideModal(that) {
    this.$element
      .hide()
      .trigger('hidden')

    backdrop.call(this)
  }

  function backdrop(callback) {
    var that = this
      , animate = this.$element.hasClass('fade') ? 'fade' : ''
    if (this.isShown && this.settings.backdrop) {
      var doAnimate = $.support.transition && animate

      this.$backdrop = $('<div class="modal-backdrop ' + animate + '" />')
        .appendTo(document.body)

      if (this.settings.backdrop != 'static') {
        this.$backdrop.click($.proxy(this.hide, this))
      }

      if (doAnimate) {
        this.$backdrop[0].offsetWidth // force reflow
      }

      this.$backdrop.addClass('in')

      doAnimate ?
        this.$backdrop.one(transitionEnd, callback) :
        callback()

    } else if (!this.isShown && this.$backdrop) {
      this.$backdrop.removeClass('in')

      $.support.transition && this.$element.hasClass('fade') ?
        this.$backdrop.one(transitionEnd, $.proxy(removeBackdrop, this)) :
        removeBackdrop.call(this)

    } else if (callback) {
      callback()
    }
  }

  function removeBackdrop() {
    this.$backdrop.remove()
    this.$backdrop = null
  }

  function escape() {
    var that = this
    if (this.isShown && this.settings.keyboard) {
      $(document).bind('keyup.modal', function (e) {
        if (e.which == 27) {
          that.hide()
        }
      })
    } else if (!this.isShown) {
      $(document).unbind('keyup.modal')
    }
  }


  /* MODAL PLUGIN DEFINITION
  * ======================= */

  $.fn.modal = function (options) {
    var modal = this.data('modal')

    if (!modal) {

      if (typeof options == 'string') {
        options = {
          show: /show|toggle/.test(options)
        }
      }

      return this.each(function () {
        $(this).data('modal', new Modal(this, options))
      })
    }

    if (options === true) {
      return modal
    }

    if (typeof options == 'string') {
      modal[options]()
    } else if (modal) {
      modal.toggle()
    }

    return this
  }

  $.fn.modal.Modal = Modal

  $.fn.modal.defaults = {
    backdrop: false
  , keyboard: false
  , show: false
  }


  /* MODAL DATA- IMPLEMENTATION
  * ========================== */

  $(document).ready(function () {
    $('body').delegate('[data-controls-modal]', 'click', function (e) {
      e.preventDefault()
      var $this = $(this).data('show', true)
      $('#' + $this.attr('data-controls-modal')).modal($this.data())
    })
  })

} (window.jQuery || window.ender);

//     Twitter Bootstrap jQuery Plugins - Modal Responsive Fix
//     Copyright (c) 2012 Nick Baugh <niftylettuce@gmail.com>
//     MIT Licensed
//     v0.0.4

// * Author: [@niftylettuce](https://twitter.com/#!/niftylettuce)
// * Source: <https://github.com/niftylettuce/twitter-bootstrap-jquery-plugins>

// # modal-responsive-fix

; (function ($, window) {

  $.fn.modalResponsiveFix = function (opts) {

    // set default options
    opts = opts || {}
    opts.spacing = opts.spacing || 10
    opts.debug = opts.debug || false
    opts.event = opts.event || 'show'

    // loop through given modals
    var $modals = $(this)
    $modals.each(loop)

    function loop() {

      var $that = $(this)

      // support for bootstrap-image-gallery
      var gallery = $that.hasClass('modal-gallery')
        , fullscreen = $that.hasClass('modal-fullscreen')
        , stretch = $that.hasClass('modal-fullscreen-stretch')

      //
      // we have to delay because modal isn't shown yet
      //  and we're trying to prevent double scrollbar
      //  on phones and tablets (see below)
      //
      // if we didn't delay, then we wouldn't get proper
      //  values for $header/$body/$footer outerHeight's
      //
      $that.on(opts.event, function (ev) {
        setTimeout(adjust($that, gallery, fullscreen, stretch), 1)
        // when we resize we want it to adjust accordingly
        $(window).on('resize.mrf', adjust($that, gallery, fullscreen, stretch))
        if (gallery) $that.on('displayed', adjust($that, gallery, fullscreen, stretch))
      })

      $that.on('hide', function () {
        $(window).off('resize.mrf', gallery, fullscreen, stretch);
      })

    }

    function adjust($el, gallery, fullscreen, stretch) {

      return function (ev) {

        var $modal = $el || $(this)
          , $header = $modal.find('.modal-header')
          , $body = $modal.find('.modal-body')
          , $footer = $modal.find('.modal-footer')

        // set the window once
        var $w = $(window)

        // set basic data like width and height
        var data = {
          width: $w.width()
          , height: $w.height()
        }

        if (gallery && $modal.data().modal._loadImageOptions && typeof ev === 'object' && ev.type === 'resize') {
          var options = $modal.data().modal._loadImageOptions
          if (fullscreen) {
            options.maxWidth = data.width
            options.maxHeight = data.height
            if (stretch) {
              options.minWidth = data.width
              options.minHeight = data.height
            }
          } else {
            options.maxWidth = data.width - $modal.data().modal._loadOptions.offsetWidth
            options.maxHeight = data.height - $modal.data().modal._loadOptions.offsetHeight
          }
          if (data.width > 480) {
            $modal.css({
              marginTop: -($modal.outerHeight() / 2)
              , marginLeft: -($modal.outerWidth() / 2)
            })
          }

          var original = {
            width: $modal.data().modal.img.width
            , height: $modal.data().modal.img.height
          }

          var img = $modal.data().modal._loadingImage = window.loadImage.scale(original, options)

          $modal.find('.modal-image').css(img)
          $modal.find('.modal-image img').attr('width', img.width)
          $modal.find('.modal-image img').attr('height', img.height)

        }

        data.scrollTop = $w.scrollTop()

        // set max height using data.height
        data.maxHeight = data.height - (opts.spacing * 2)

        // adjust max height for tablets
        if (data.width > 480 && data.width <= 767)
          data.maxHeight = data.maxHeight - 20

        var modal = {
          width: $modal.width()
          , height: $modal.height()
        }

        // take data.maxHeight and subtract the height footer/header/body padding
        if (!gallery) {
          var difference = data.maxHeight
          difference = difference - $header.outerHeight(true)
          difference = difference - $footer.outerHeight(true)
          difference = difference - ($body.outerHeight(true) - $body.height())
          if (difference > 400 && data.width > 480 && data.width <= 767)
            difference = 400
          else
            difference = difference - 50
          $body.css('max-height', difference)
        }

        if (modal.height >= data.maxHeight) {
          modal.top = (gallery && fullscreen) ? data.scrollTop : data.scrollTop + opts.spacing
        } else {
          modal.top = data.scrollTop + (data.height - modal.height) / 2
        }
        $modal.css({ top: modal.top, position: 'absolute', marginTop: 0 })

        // ## debug info
        if (opts.debug) {
          var output = {
            options: opts
            , data: data
            , modal: modal
          }
          console.log('modalResponsiveFix', output)
        }

      }
    }
  }
})(jQuery, window)