(function ($) {
    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();


    // Sticky Navbar
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.sticky-top').css('top', '0px');
        } else {
            $('.sticky-top').css('top', '-100px');
        }
    });

    // Dropdown on mouse hover (desktop) and click (mobile)
    const $dropdown = $(".dropdown");
    const $dropdownToggle = $(".dropdown-toggle");
    const $dropdownMenu = $(".dropdown-menu");
    const showClass = "show";

    $(window).on("load resize", function () {
        if (this.matchMedia("(min-width: 992px)").matches) {
            // Desktop: Hover functionality
            $dropdown.hover(
                function () {
                    const $this = $(this);
                    $this.addClass(showClass);
                    $this.find($dropdownToggle).attr("aria-expanded", "true");
                    $this.find($dropdownMenu).addClass(showClass);
                },
                function () {
                    const $this = $(this);
                    $this.removeClass(showClass);
                    $this.find($dropdownToggle).attr("aria-expanded", "false");
                    $this.find($dropdownMenu).removeClass(showClass);
                }
            );

            // Remove click functionality for desktop
            $dropdownToggle.off("click");
        } else {
            // Mobile: Click functionality
            $dropdown.off("mouseenter mouseleave"); // Remove hover functionality

            $dropdownToggle.on("click", function (e) {
                e.preventDefault(); // Prevent default link behavior
                const $this = $(this);
                const $parent = $this.parent();

                // Toggle the 'show' class
                $parent.toggleClass(showClass);

                // Update aria-expanded
                const isExpanded = $parent.hasClass(showClass);
                $this.attr("aria-expanded", isExpanded);

                // Close other dropdowns when one is opened
                $dropdown.not($parent).removeClass(showClass);
                $dropdownToggle.not($this).attr("aria-expanded", "false");
            });

            // Close dropdown when clicking outside
            $(document).on("click", function (e) {
                if (!$(e.target).closest($dropdown).length) {
                    $dropdown.removeClass(showClass);
                    $dropdownToggle.attr("aria-expanded", "false");
                }
            });
        }
    });
  

    /**
     * Scroll top button
     */
    const scrollTop = document.querySelector('.scroll-top');
    if (scrollTop) {
        const togglescrollTop = function () {
            window.scrollY > 100 ? scrollTop.classList.add('active') : scrollTop.classList.remove('active');
        }
        window.addEventListener('load', togglescrollTop);
        document.addEventListener('scroll', togglescrollTop);
        scrollTop.addEventListener('click', window.scrollTo({
            top: 0,
            behavior: 'smooth'
        }));
    }
  })(jQuery);

