function fadeToBlack() {
    if (window.__overlay != null)
        return;

    window.__overlay = document.createElement("div");

    window.__overlay.className = "overlay";

    document.body.appendChild(window.__overlay);

    document.body.style.overflow = "hidden";

    setTimeout(function () {
        window.__overlay.classList.add("visible");
    }, 1);
}

function fadeToWhite() {
    if (window.__overlay == null)
        return;

    window.__overlay.classList.remove("visible");

    setTimeout(function () {
        document.body.removeChild(window.__overlay);

        document.body.style.overflow = "auto";

        window.__overlay = null;
    }, 250);
}

function pageBeginRequest(sender, eventArgs) {
    Sys.WebForms.PageRequestManager.getInstance()._scrollPosition = null;

    if (window.__busyOverlay == null) {
        window.__busyOverlay = document.createElement("div");

        window.__busyOverlay.id = "busyOverlay";

        document.body.appendChild(window.__busyOverlay);

        window.__busyTimeout = setTimeout(function () {
            if (window.__busyOverlay != null) {
                window.__busyOverlay.innerHTML = "<svg xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" width=\"60\" height=\"60\" viewBox=\"0 0 60 60\"><path d=\"M37.275,52.641a14.864,14.864,0,0,1,1.165-5.773A15.569,15.569,0,0,1,52.771,37.5a15.735,15.735,0,0,1,2.988.292A15.185,15.185,0,1,1,37.275,52.641M48.239,28.188a24.579,24.579,0,0,0-3.736.976.325.325,0,0,0-.207.222l-.844,3.14-.243.917.117-.067a.315.315,0,0,0-.15.2l.033-.13-2.481,1.43.138.047a.326.326,0,0,0-.264.027l.126-.074-3.814-1.265a.328.328,0,0,0-.312.06,25.262,25.262,0,0,0-2.754,2.718.33.33,0,0,0-.069.3l.8,3,.282,1.054.066-.117a.334.334,0,0,0-.033.246l-.033-.129-.568.988-.862,1.491.141-.028a.311.311,0,0,0-.21.149l0,.006,0-.006.069-.122-1.114.229-2.835.584a.327.327,0,0,0-.237.209,24.1,24.1,0,0,0-1.018,3.725.325.325,0,0,0,.084.291l2.97,2.968V51.08a.327.327,0,0,0,.1.23l-.1-.1v2.861l.108-.1a.34.34,0,0,0-.108.243v-.147l-3.009,2.677a.328.328,0,0,0-.105.3,25.315,25.315,0,0,0,.976,3.739.334.334,0,0,0,.223.206l2.1.559,1.964.529-.058-.1-.008-.014.008.014a.315.315,0,0,0,.185.137l-.126-.034,1.432,2.478.043-.133,0-.006,0,.006a.337.337,0,0,0,.026.259l-.069-.126L33.6,68.378a.309.309,0,0,0,.06.311,25.13,25.13,0,0,0,2.715,2.755.334.334,0,0,0,.3.068l1.261-.338,2.79-.749-.11-.063-.007,0,.007,0a.331.331,0,0,0,.239.031l-.129.032,2.105,1.216.375.217-.03-.143a.328.328,0,0,0,.156.215l-.126-.072.811,3.946a.316.316,0,0,0,.207.237,24.519,24.519,0,0,0,3.727,1.021.315.315,0,0,0,.288-.089l2.97-2.965h-.135a.332.332,0,0,0,.231-.1l-.1.1h2.862l-.1-.109a.335.335,0,0,0,.243.109h-.147l.46.512,2.216,2.5a.312.312,0,0,0,.3.1,24.659,24.659,0,0,0,3.736-.973.324.324,0,0,0,.207-.223l1.09-4.058-.12.068a.323.323,0,0,0,.153-.2l-.033.128.691-.4,1.784-1.03-.135-.046a.331.331,0,0,0,.261-.029l-.126.075L68.363,71.7a.336.336,0,0,0,.318-.063,25.232,25.232,0,0,0,2.748-2.716.315.315,0,0,0,.069-.3l-.342-1.278-.742-2.775-.065.109,0,.008,0-.008a.305.305,0,0,0,.03-.231l0-.006,0,.006.034.122.354-.612,1.078-1.866-.144.029a.31.31,0,0,0,.209-.147l0-.008,0,.008-.064.118,3.348-.692.6-.122a.324.324,0,0,0,.237-.208,24.837,24.837,0,0,0,1.021-3.725.336.336,0,0,0-.09-.291l-2.042-2.041-.925-.927v.134a.34.34,0,0,0-.1-.23l.1.1V51.227l-.108.095a.329.329,0,0,0,.108-.242v.147l1.447-1.291L77,48.551a.31.31,0,0,0,.1-.3,24.522,24.522,0,0,0-.976-3.739.322.322,0,0,0-.219-.207l-2.565-.686-1.5-.4.066.118a.3.3,0,0,0-.186-.148l-.009,0,.009,0,.12.031-1.43-2.479-.048.139a.316.316,0,0,0-.023-.258l0-.007,0,.007.071.119.216-.652,1.051-3.163a.326.326,0,0,0-.063-.311,25.1,25.1,0,0,0-2.715-2.754.329.329,0,0,0-.3-.066L64.561,34.88l.107.063.007,0-.007,0a.305.305,0,0,0-.228-.029l-.005,0,.005,0,.121-.034-2.477-1.432.024.144a.309.309,0,0,0-.153-.215l.129.072L61.27,29.5a.318.318,0,0,0-.207-.237,24.654,24.654,0,0,0-3.727-1.022.321.321,0,0,0-.288.09L54.08,31.3h.132a.319.319,0,0,0-.228.1l.1-.1H51.212l.1.109a.334.334,0,0,0-.24-.109h.141L48.54,28.29a.333.333,0,0,0-.242-.108.31.31,0,0,0-.058.006\" transform=\"translate(-22.695 -22.706)\" fill=\"#00b1d4\" /></svg>";
            }
        }, 1000);
    }
}

function pageEndRequest(sender, eventArgs) {
    if (window.__busyTimeout != null) {
        clearTimeout(window.__busyTimeout);

        window.__busyTimeout = null;
    }

    if (window.__busyOverlay != null) {
        document.body.removeChild(window.__busyOverlay);

        window.__busyOverlay = null;
    }

    if (eventArgs.get_error() != undefined) {
        eventArgs.set_errorHandled(true);

        snackbar("error", "An error occurred while processing your request. Our support team has been notified, and will fix the problem as soon as possible. We apologise for the inconvenience.");
    }
}

function snackbar(className, message) {
    function snackbar_click() {
        var self = this;

        self.style.opacity = "0";

        setTimeout(function () {
            self.removeEventListener("click", snackbar_click);

            window.__snackbar_container.removeChild(self);

            if (window.__snackbar_container.childNodes.length == 0) {
                document.body.removeChild(window.__snackbar_container);

                delete window.__snackbar_container;
            }
        }, 150);
    }

    if (window.__snackbar_container == undefined) {
        function snackbar_container_mouseout() {
            window.__snackbar_container.__timeouts = [];

            for (let div of window.__snackbar_container.childNodes) {
                window.__snackbar_container.__timeouts.push(setTimeout(snackbar_click.bind(div), 5000));
            }
        }

        function snackbar_container_mouseover() {
            for (let timeout of window.__snackbar_container.__timeouts) {
                clearTimeout(timeout);
            }

            window.__snackbar_container.__timeouts = [];
        }

        window.__snackbar_container = document.createElement("div");

        window.__snackbar_container.__timeouts = [];

        window.__snackbar_container.className = "snackbar_container";

        document.body.appendChild(window.__snackbar_container);

        window.__snackbar_container.addEventListener("mouseout", snackbar_container_mouseout);
        window.__snackbar_container.addEventListener("mouseover", snackbar_container_mouseover);
    }

    var div = document.createElement("div");

    div.classList.add("snackbar");
    div.classList.add(className);
    div.innerText = message;

    div.addEventListener("click", snackbar_click);

    while (window.__snackbar_container.childNodes.length > 4) {
        window.__snackbar_container.removeChild(window.__snackbar_container.firstChild);
    }

    if (window.__snackbar_container.childNodes.length == 0) {
        window.__snackbar_container.appendChild(div);
    }
    else {
        window.__snackbar_container.insertBefore(div, window.__snackbar_container.childNodes[0]);
    }

    setTimeout(function () {
        div.style.opacity = "1";
    }, 1);

    window.__snackbar_container.__timeouts.push(setTimeout(snackbar_click.bind(div), 5000));
}

function PopupHelper(parent, child, options) {
    this.child = child;
    this.options = options;
    this.parent = parent;

    this.mousechild = false;
    this.mouseparent = false;
    this.visible = false;

    this.bound_child_mousedown = this.child_mousedown.bind(this);
    this.bound_child_mouseup = this.child_mouseup.bind(this);
    this.bound_child_scroll = this.child_scroll.bind(this);
    this.bound_parent_blur = this.parent_blur.bind(this);
    this.bound_parent_focus = this.parent_focus.bind(this);
    this.bound_parent_input = this.parent_input.bind(this);
    this.bound_parent_keydown = this.parent_keydown.bind(this);
    this.bound_parent_keyup = this.parent_keyup.bind(this);
    this.bound_parent_mousedown = this.parent_mousedown.bind(this);
    this.bound_window_mousedown = this.window_mousedown.bind(this);
    this.bound_window_mouseup = this.window_mouseup.bind(this);
    this.bound_window_resize = this.window_resize.bind(this);
    this.bound_window_scroll = this.window_scroll.bind(this);

    this.child.addEventListener("mousedown", this.bound_child_mousedown);
    this.child.addEventListener("mouseup", this.bound_child_mouseup);
    this.child.addEventListener("scroll", this.bound_child_scroll);
}

PopupHelper.prototype.child_mousedown = function () {
    this.mousechild = true;

    event.cancelBubble = true;
};

PopupHelper.prototype.child_mouseup = function () {
    this.mousechild = false;

    if (this.options.mode == "focus") {
        this.parent.focus();
    }
};

PopupHelper.prototype.child_scroll = function () {
    event.cancelBubble = true;
};

PopupHelper.prototype.hide = function () {
    this.visible = false;

    this.child.style.display = "none";

    this.parent.classList.remove("focus");

    window.removeEventListener("mousedown", this.bound_window_mousedown);
    window.removeEventListener("resize", this.bound_window_resize);
    window.removeEventListener("scroll", this.bound_window_scroll);

    if (this.options.onhide) {
        this.options.onhide(this.parent, this.child, this.options.data);
    }

    this.parent.blur();
};

PopupHelper.prototype.layout = function () {
    var rect = this.parent.getBoundingClientRect();

    this.child.style.left = rect.left + "px";
    this.child.style.top = rect.bottom + "px";

    if (this.child.offsetLeft + this.child.offsetWidth >= document.documentElement.clientWidth &&
        rect.right - this.child.offsetWidth >= 0) {
        this.child.style.left = rect.right - this.child.offsetWidth + "px";
    }

    if (this.child.offsetTop + this.child.offsetHeight >= document.documentElement.clientHeight &&
        rect.top - this.child.offsetHeight >= 0) {
        this.child.style.top = rect.top - this.child.offsetHeight + "px";
    }
};

PopupHelper.prototype.parent_blur = function () {
    if (this.options.mode == "focus" && this.visible && !this.mousechild) {
        this.hide();
    }
};

PopupHelper.prototype.parent_focus = function () {
    if (this.options.mode == "focus" && !this.visible) {
        this.show();
    }
};

PopupHelper.prototype.parent_input = function () {
    if (this.options.oninput) {
        this.options.oninput(this.parent, this.child, this.options.data);
    }
};

PopupHelper.prototype.parent_keydown = function () {
    if (this.options.onkeydown) {
        this.options.onkeydown(this.parent, this.child, this.options.data);
    }
};

PopupHelper.prototype.parent_keyup = function () {
    if (this.options.onkeyup) {
        this.options.onkeyup(this.parent, this.child, this.options.data);
    }
};

PopupHelper.prototype.parent_mousedown = function () {
    this.mouseparent = true;

    window.addEventListener("mouseup", this.bound_window_mouseup);

    if (this.options.mode == "click") {
        if (this.visible) {
            this.hide();
        }
        else {
            this.show();
        }
    }
};

PopupHelper.prototype.show = function () {
    if (this.options.onshow) {
        this.options.onshow(this.parent, this.child, this.options.data);
    }

    window.addEventListener("mousedown", this.bound_window_mousedown);
    window.addEventListener("resize", this.bound_window_resize);
    window.addEventListener("scroll", this.bound_window_scroll);

    this.parent.classList.add("focus");

    this.child.style.display = "block";

    this.layout();

    this.visible = true;
};

PopupHelper.prototype.window_mousedown = function () {
    if (!this.mouseparent) {
        this.hide();
    }
};

PopupHelper.prototype.window_mouseup = function () {
    window.removeEventListener("mouseup", this.bound_window_mouseup);

    this.mouseparent = false;
};

PopupHelper.prototype.window_resize = function () {
    this.layout();
};

PopupHelper.prototype.window_scroll = function () {
    this.layout();
};

PopupHelper.init = function (parent, child, options) {
    parent.removeAttribute("onblur");
    parent.removeAttribute("onfocus");
    parent.removeAttribute("onkeydown");
    parent.removeAttribute("onkeyup");
    parent.removeAttribute("oninput");
    parent.removeAttribute("onmousedown");

    parent.popupHelper = new PopupHelper(parent, child, options);

    parent.addEventListener("blur", parent.popupHelper.bound_parent_blur);
    parent.addEventListener("focus", parent.popupHelper.bound_parent_focus);
    parent.addEventListener("input", parent.popupHelper.bound_parent_input);
    parent.addEventListener("keydown", parent.popupHelper.bound_parent_keydown);
    parent.addEventListener("keyup", parent.popupHelper.bound_parent_keyup);
    parent.addEventListener("mousedown", parent.popupHelper.bound_parent_mousedown);

    if (options.mode == "click") {
        parent.popupHelper.parent_mousedown();
    }
    else if (options.mode == "focus") {
        parent.popupHelper.parent_focus();
    }
};
